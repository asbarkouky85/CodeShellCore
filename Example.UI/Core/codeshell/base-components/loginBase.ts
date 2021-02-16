import { BaseComponent } from "./baseComponent";
import { AccountServiceBase } from "codeshell/security";
import { Shell,Utils } from "codeshell";
import { Component } from '@angular/core';


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
        if (Shell.Session.IsLoggedIn)
            this.Router.navigateByUrl("/");

        this.Title = Shell.Translator.instant("Words.Login");
    }

    Login() {

        this.AccountService.Login(this.model).then(data => {
            Shell.Session.StartSession(data);
            this.Router.navigateByUrl("/");
        }).catch(error => {
            Utils.HandleError(error, true);
        })
    }
}