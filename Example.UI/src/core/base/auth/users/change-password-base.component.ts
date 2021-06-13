import { Component, Injectable } from "@angular/core";
import { Shell } from "codeshell";
import { EditComponentBase } from "codeshell/base-components";
import { AccountServiceBase } from "codeshell/http";
import { SubmitResult } from "codeshell/results";
import { UsersService } from "../http";

@Component({ template: '' })
export abstract class ChangePasswordBase extends EditComponentBase {
    Service = new UsersService();
    get AccountService(): AccountServiceBase { return Shell.Injector.get(AccountServiceBase); }

    SubmitAsync(): Promise<SubmitResult> {
        return this.AccountService.ChangePassword(this.model);
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