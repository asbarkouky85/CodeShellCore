import { LoginResult, UserDTO, TokenData, Permission, RouteData } from "CodeShell/Security/Models";
import { Injectable } from "@angular/core";
import { Stored } from "CodeShell/Helpers";
import { Registry } from "../Utilities/Registry";
import * as Cookies from "js-cookie";
import { String_GetAfterLast } from "../Utilities/Utils";

@Injectable()
export class SessionManager {

    private UserData: UserDTO | null | undefined;
    private static _instance: SessionManager;
    public IsLoggedIn = false;
    private _tokenData: TokenData | null = null;

    //public get IsLoggedIn(): boolean { return this._loggedIn; }

    public get User(): UserDTO {
        let init = Registry.UserType;
        if (this.UserData == undefined) {
            this.UserData = Stored.Get("UserData", init) as UserDTO;
            this.MapPermissions(this.UserData);
        }
        return this.UserData;
    }

    private MapPermissions(data: any) {
        for (var i in data.permissions) {
            var item = new Permission;
            Object.assign(item, data.permissions[i]);
            data.permissions[i] = item;
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
        this.UserData = data.userData;
        this.MapPermissions(this.UserData);

        this._tokenData = Object.assign(new TokenData,{
            Token: data.token,
            Expiry: data.tokenExpiry
        });

        localStorage.setItem("TokenData", JSON.stringify(this._tokenData));
        localStorage.setItem("UserData", JSON.stringify(data.userData));
        this.IsLoggedIn = true;
    }

    public EndSession() {
        localStorage.removeItem("TokenData");
        localStorage.removeItem("UserData");
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
            return u.permissions[id];
        } else {
            let s = new Permission;
            s.view = false;
            return s;
        }
    }

    private CheckToken(): void {
        this._tokenData = Stored.Get("TokenData", TokenData);
        if (this._tokenData == null) {
            this.IsLoggedIn = false;
        } else {
            let token = this._tokenData as TokenData;
            this.IsLoggedIn = new Date() < new Date(token.Expiry);
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

    public IsAuthorized(data: RouteData): boolean {
        if (data.IsAnonymous)
            return true;
        if (this.IsLoggedIn && data.AllowAll)
            return true;

        let p = this.GetPermission(data.resource);
        return ((p.Can(data.action) && this.CheckRole(data)) || this.CheckUserType(data));
    }

    CheckUserType(data: RouteData): boolean {
        if (typeof data.action != "string")
            return false;

        let type = data.action as string;
        if (type.indexOf("UserType") != 0)
            return false;

        return ("UserType." + this.User.userTypeString) == type;
    }

    CheckRole(data: RouteData): boolean {
        if (!data.apps)
            return true;

        for (let uRole of this.User.apps) {
            for (let role of data.apps) {
                if (role == uRole)
                    return true;
            };
        }
        return false;
    }
}