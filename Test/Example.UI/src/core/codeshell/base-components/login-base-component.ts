import { BaseComponent } from "./base-component";
import { SessionManager } from "codeshell/security";
import { Shell } from "codeshell/shell";
import { Utils } from "codeshell/utilities/utils";
import { Component } from '@angular/core';
import { AccountServiceBase } from 'codeshell/http';


@Component({ template: '' })
export class LoginBase extends BaseComponent {
    AccountService: AccountServiceBase = Shell.Injector.get(AccountServiceBase);
    model: any = {};
    ForgotPasswordUrl?: string;

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

    ngOnInit() {
        super.ngOnInit();
        if (SessionManager.Current.IsLoggedIn)
            this.Router.navigateByUrl("/");

        this.Title = Shell.Translator.instant("Words.Login");
    }

    Login() {

        this.AccountService.Login(this.model).then(data => {
            SessionManager.Current.StartSession(data);
            this.Router.navigateByUrl("/");
        }).catch(error => {
            Utils.HandleError(error, true);
        })
    }
}