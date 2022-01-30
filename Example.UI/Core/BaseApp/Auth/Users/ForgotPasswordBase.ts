import { EditComponentBase } from "codeshell/baseComponents";
import { Injectable } from "@angular/core";
import { UsersService } from "BaseApp/Auth/Http";
import { AccountServiceBase } from "codeshell/security";
import { Shell } from "codeshell/core";

@Injectable()
export abstract class ForgotPasswordBase extends EditComponentBase{
    get Service(): AccountServiceBase { return Shell.Injector.get(AccountServiceBase); }

    SubmitAsync() {
        return this.Service.SendResetMail(this.model);
    }
}