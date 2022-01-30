import { EditComponentBase } from "codeshell/baseComponents";
import { Injectable } from "@angular/core";
import { UsersService } from "../Http";
import { Shell } from "codeshell/core";
import { Router } from "@angular/router";
import { SubmitResult, NoteType, Utils } from "codeshell/helpers";
import { AccountServiceBase } from "codeshell/security";

@Injectable()
export abstract class ChangePasswordBase extends EditComponentBase{
    get Service(): AccountServiceBase { return Shell.Injector.get(AccountServiceBase); }

    SubmitAsync(): Promise<SubmitResult> {
        return this.Service.ChangePassword(this.model);
    }

    ShowPassword(passInput: HTMLInputElement) {
        passInput.type = "text";
    }

    HidePassword(passInput: HTMLInputElement) {
        passInput.type = "password";
    }

    TogglePassword(passInput: HTMLInputElement) {
        if (passInput.type == "text")
            passInput.type = "password";
        else
            passInput.type = "text";
    }
}