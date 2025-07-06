import { UserDTO, RouteData, Permission } from "./models";
import { Shell } from "../shell";
import { EventEmitter } from "@angular/core";

export class AuthorizationError {
    message: string = "";
    data?: RouteData;
}

export class AuthorizationServiceBase {
    static OnAuthorizationFailed: EventEmitter<AuthorizationError> = new EventEmitter<AuthorizationError>();
    User?: UserDTO;
    constructor() {

    }

    public IsAuthorized(user: UserDTO | undefined, data: RouteData, events: boolean = false) {
        if (data.IsAnonymous)
            return true;
        this.User = user;
        if (this.User) {
            if (!this.CheckApp(data)) {
                if (events)
                    AuthorizationServiceBase.OnAuthorizationFailed.emit({ data: data, message: "Incompatible Apps" });
                return false;
            }
            if (data.AllowAll)
                return true;
            if (this.CheckUserType(data))
                return true;
            let p = this.GetPermission(data.resource);

            if (!p.Can(data.action)) {
                if (events)
                    AuthorizationServiceBase.OnAuthorizationFailed.emit({ data: data, message: "No permission" });
                return false;
            }
            return true;
        }
        return false;
    }

    public async IsAuthorizedAsync(data: RouteData): Promise<boolean> {
        if (data.IsAnonymous)
            return true;

        var user = await Shell.Session.GetUserAsync();
        return this.IsAuthorized(user, data,true);
    }

    public GetPermission(id: string): Permission {
        if (this.User && this.User.permissions[id]) {
            let u = this.User;
            return u.permissions[id];
        } else {
            let s = new Permission;
            s.view = false;
            return s;
        }
    }

    CheckApp(data: RouteData): boolean {

        if (this.User && data.apps) {
            for (let d of data.apps) {
                if (this.User.app == d)
                    return true;
            }
            return false;
        }

        return true;
    }

    CheckUserType(data: RouteData): boolean {
        if (!this.User)
            return false;
        if (typeof data.action != "string")
            return false;

        let type = data.action as string;
        if (type.indexOf("UserType") != 0)
            return false;

        return ("UserType." + this.User.userTypeString) == type;
    }

}