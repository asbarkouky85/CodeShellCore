import { BaseComponent } from "../base-components";
import { ActivatedRoute } from "@angular/router";
import { UserDTO } from "../security/models";

export class Registry{
    private static List: { [index: string]: new (...args:any[]) => BaseComponent } = {};
    private static _userType?: new () => UserDTO;

    public static get UserType(): new () => UserDTO {
        if (this._userType)
            return this._userType;
        else
            return UserDTO;
    };


    static Register<T extends BaseComponent>(name: string, s: new(...args:any[]) => T) {
        Registry.List[name] = s;
    }

    static Get(name: string) : (new (...args:any[]) => BaseComponent) | undefined  {
        if (Registry.List[name])
            return Registry.List[name];
        return undefined;
    }

    static RegisterUserType<T extends UserDTO>(con: new () => T) {
        Registry._userType = con;
    }
}