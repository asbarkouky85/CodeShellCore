import { LoginResult, UserDTO, TokenData } from "../security/models";
import { Injectable, EventEmitter } from "@angular/core";
import { SubmitResult } from "../results";
import * as Cookies from "js-cookie";
import { String_GetAfterLast } from "../utilities/functions";
import { Shell } from "../shell";
import { AccountServiceBase } from "../http/accountServiceBase";
import { TokenStorage } from "./tokenStorage";
import { Permission } from './permission';

var userData: UserDTO | null = null;
var loaded: boolean = false;

@Injectable()
export class SessionManager {

    private static _instance: SessionManager;
    private static _loadPromise?: Promise<UserDTO>;
    private _tokenData: TokenData | null = null;
    private _tokenStorage?: TokenStorage;

    IsLoggedIn = false;
    LogStatus: EventEmitter<boolean> = new EventEmitter<boolean>(false);
    OnLogin: EventEmitter<UserDTO> = new EventEmitter<UserDTO>();
    OnUserDataFailed: EventEmitter<SubmitResult> = new EventEmitter<SubmitResult>();

    private get TokenStorage() {
        if (!this._tokenStorage)
            this._tokenStorage = Shell.Injector.get(TokenStorage);
        return this._tokenStorage;
    }

    static StartApp() {
        this._instance = new SessionManager;
        this._instance.GetDeviceId();
        this._instance.CheckToken();
    }

    get User(): UserDTO {
        if (!userData)
            throw "Not logged in";
        return userData as UserDTO;
    }

    async TryRefreshAsync(): Promise<UserDTO> {
        var ref = this.TokenStorage.GetRefreshToken();
        if (ref) {
            var serv: AccountServiceBase = Shell.Injector.get(AccountServiceBase);
            try {
                var data = await serv.RefreshToken(ref);

                this.StartSession(data);
            } catch (e) {
                Promise.reject("Failed to refresh using token");
            }
        }
        return Promise.reject("No token or refresh token found")
    }

    async ReloadUserDataAsync(): Promise<UserDTO> {
        var token = this.GetToken();
        if (!token)
            return Promise.reject("Cannot reload user data without token");
        await this.GetUserDataFromServer();
        if (userData) {
            this.MapPermissions(userData);
            this.OnLogin.emit(userData);
        }
        return userData as UserDTO;

    }

    GetUserAsync(): Promise<UserDTO> {
        if (userData) {
            return Promise.resolve(userData);
        }

        if (!SessionManager._loadPromise) {
            var token = this.GetToken();
            if (!token) {
                SessionManager._loadPromise = this.TryRefreshAsync();
            } else {
                SessionManager._loadPromise = this.GetUserDataFromServer();
            }

        }
        return SessionManager._loadPromise;
    }

    private async GetUserDataFromServer(): Promise<UserDTO> {
        try {
            var serv: AccountServiceBase = Shell.Injector.get(AccountServiceBase);
            userData = await serv.GetUserData();
            this.MapPermissions(userData);
            return Promise.resolve(userData);
        } catch (e) {
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

    public static get Current(): SessionManager {
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

        this.TokenStorage.SaveToken(this._tokenData);

        if (data.refreshToken)
            this.TokenStorage.SaveRefreshToken(data.refreshToken);

        this.OnLogin.emit(userData);
        this.LogStatus.emit(true);
        this.IsLoggedIn = true;
    }

    public CheckToken(): void {
        this._tokenData = this.TokenStorage.LoadToken();
        if (this._tokenData == null) {
            this.IsLoggedIn = false;
        } else {
            let token = this._tokenData as TokenData;
            this.IsLoggedIn = new Date() < new Date(token.Expiry);
        }

        this.LogStatus.emit(this.IsLoggedIn);
    }

    public EndSession() {
        this.TokenStorage.Clear();
        userData = null;
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


    public GetToken(): TokenData | null {
        if (!this.IsLoggedIn) {
            return null;
        }
        if (this._tokenData && this._tokenData.IsExpired())
            return null;

        return this._tokenData;
    }


}