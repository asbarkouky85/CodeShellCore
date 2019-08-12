import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'codeshell/baseComponents';
import { Shell } from 'codeshell/core';
import { AccountServiceBase } from 'codeshell/security';
import { NoteType } from 'codeshell/helpers';

@Component({ templateUrl: "./Login.html" })
export class Login extends BaseComponent {
    loginData = {
        userName: '',
        password: ''
    };

    AccountService: AccountServiceBase = Shell.Injector.get(AccountServiceBase);

    GetPageId(): number {
        return 0;
    }

    onSubmit() {
        
        this.AccountService.Login(this.loginData).then(data => {
            if (data.success) {
                Shell.Session.StartSession(data);
                this.Router.navigateByUrl("/");
            } else {
                this.NotifyTranslate(data.message, NoteType.Error)
            }

        }).catch(error => {
            this.NotifyTranslate(error, NoteType.Error)
        })
    }
}



