import { EntityHttpService } from "./entityHttpService";
import { LoginResult, UserDTO } from "../security/models";
import { SubmitResult } from "../results";
import { HttpServiceBase } from './httpServiceBase';

export abstract class AccountServiceBase extends HttpServiceBase {
    Login(model: any): Promise<LoginResult> {
        return this.PostAs<LoginResult>("Login", model);
    }

    GetUserData(): Promise<UserDTO> {
        return this.GetAs<UserDTO>("GetUserData");
    }

    RefreshToken(token: string): Promise<LoginResult> {
        return this.PostAs<LoginResult>("RefreshToken", { token: token });
    }
    
    ChangePassword(dto: any): Promise<SubmitResult> {
        return this.PostAs<SubmitResult>("ChangePassword", dto);
    }

    SendResetMail(mail: string): Promise<SubmitResult> {
        return this.Save("SendResetMail", { email: mail });
    }

    RequestResetPassword(mail:string):Promise<SubmitResult>{
        return this.Post("RequestResetPassword",{email:mail});
    }
}
