import { EntityHttpService } from "../http";
import {LoginResult, UserDTO} from "./models";


export abstract class AccountServiceBase extends EntityHttpService { 
    Login(model:any):Promise<LoginResult>{
        return this.PostAs<LoginResult>("Login",model);
    }

    GetUserData(): Promise<UserDTO> {
        return this.GetAs<UserDTO>("GetUserData");
    }
}
