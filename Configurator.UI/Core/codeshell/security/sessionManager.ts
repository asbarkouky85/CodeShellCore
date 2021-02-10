import { LoginResult, UserDTO, TokenData, Permission, RouteData } from "../security/models";
import { Injectable, EventEmitter } from "@angular/core";
import { Stored, SubmitResult } from "../helpers";
import { Registry } from "../utilities/registry";
import * as Cookies from "js-cookie";
import { String_GetAfterLast } from "../utilities/utils";
import { Shell } from "../core";
import { AccountServiceBase } from "./accountServiceBase";

var userData: UserDTO | null = null;
var loaded: boolean = false;

@Injectable()
export class SessionManager {


    private static _instance: SessionManager;
    private static _loadPromise?: Promise<UserDTO>;
    private _tokenData: TokenData | null = null;

    IsLoggedIn = false;
    LogStatus: EventEmitter<boolean> = new EventEmitter<boolean>(false);
    OnLogin: EventEmitter<UserDTO> = new EventEmitter<UserDTO>();
    OnUserDataFailed: EventEmitter<SubmitResult> = new EventEmitter<SubmitResult>();

    public get User(): UserDTO {
        if (!userData)
            throw "Not logged in";
        return userData as UserDTO;
    }

    public GetUserAsync(): Promise<UserDTO> {
        var token = this.GetToken();
        if (!token) {
            return Promise.reject("no token")
        }
            


        if (userData) {
            return Promise.resolve(userData);
        }

        if (!SessionManager._loadPromise)
            SessionManager._loadPromise = this.GetUserDataFromServer();

        return SessionManager._loadPromise;
    }

    private async GetUserDataFromServer(): Promise<UserDTO> {
        try {
            var serv: AccountServiceBase = Shell.Injector.get(AccountServiceBase);
            userData = await serv.GetUserData();
            //console.log("FROM SERVER ", userData)
            this.MapPermissions(userData);
            return Promise.resolve(userData);
        } catch (e) {
            console.log(e);
            var res: SubmitResult = new SubmitResult;
            res.code = 1;
            res.message = "unable_to_connect_to_server";
            if (e.error && e.error.code) {
                res = Object.assign(new SubmitResult, e.error);
            }
            this.OnUserDataFailed.emit(res);
            return Promise.reject(e);
        }

    }

    private MapPermissions(data: any) {
        if (data) {
            for (var i in data.permissions) {
                var item = new Permission;
                Object.assign(item, data.permissions[i]);
                data.permissions[i] = item;
            }
        }
    }

    public static Current(): SessionManager {
        if (this._instance == null) {
            this._instance = new SessionManager;
            this._instance.CheckToken();
        }
        return this._instance;
    }

    public StartSession(data: LoginResult) {
        userData = data.userData;
        this.MapPermissions(userData);

        this._tokenData = Object.assign(new TokenData, {
            Token: data.token,
            Expiry: data.tokenExpiry
        });

        localStorage.setItem("TokenData", JSON.stringify(this._tokenData));
        //localStorage.setItem("UserData", JSON.stringify(data.userData));
        this.OnLogin.emit(userData);
        this.LogStatus.emit(true);
        this.IsLoggedIn = true;
    }

    public EndSession() {
        localStorage.removeItem("TokenData");
        //localStorage.removeItem("UserData");

        this.LogStatus.emit(false);
        this.IsLoggedIn = false;
        this._tokenData = null;
    }

    public GetDeviceId(): string {
        var id = Cookies.get("DID");
        if (!id) {
            let d = new Date();
            let no = Math.random().toString();
            id = new Date().getTime().toString() + '_' + String_GetAfterLast(no, ".");
            d.setFullYear(d.getFullYear() + 3);
            Cookies.set("DID", id, { path: "/", expires: d });
        }

        return id;
    }


    public GetPermission(id: string): Permission {

        if (this.IsLoggedIn && this.User.permissions[id]) {
            let u = this.User;

            return Object.assign(new Permission, u.permissions[id]);
        } else {
            let s = new Permission;
            s.view = false;
            return s;
        }
    }

    public CheckToken(): void {
        this._tokenData = Stored.Get("TokenData", TokenData);
        if (this._tokenData == null) {
            this.IsLoggedIn = false;
        } else {
            let token = this._tokenData as TokenData;
            this.IsLoggedIn = new Date() < new Date(token.Expiry);
        }

        this.LogStatus.emit(this.IsLoggedIn);
    }

    public GetToken(): TokenData | null {
        if (!this.IsLoggedIn) {
            return null;
        }
        if (this._tokenData && this._tokenData.IsExpired())
            return null;

        return this._tokenData;
    }


}