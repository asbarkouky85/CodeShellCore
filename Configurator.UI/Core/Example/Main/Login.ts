import { Component } from '@angular/core';
import { BaseComponent } from 'codeshell/baseComponents';
import { Shell } from 'codeshell/core';
import { AccountServiceBase } from 'codeshell/security';
import { NoteType } from 'codeshell/helpers';

@Component({ templateUrl: "./Login.html" })
export class Login extends BaseComponent {

    AccountService: AccountServiceBase = Shell.Injector.get(AccountServiceBase);
    model: any = {};
    GetPageId(): number { return 0; }

    Login() {
        this.AccountService.Login(this.model).then(data => {
            Shell.Session.StartSession(data);
            if (data.userData.tenantId == 2)
                location.pathname = "/"
            else
                this.Router.navigateByUrl("/");
        }).catch(error => {
            this.NotifyTranslate("invalid_credentials", NoteType.Error)
        })
    }
}