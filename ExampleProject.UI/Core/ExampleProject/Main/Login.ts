import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'CodeShell/BaseComponents';
import { Shell } from 'CodeShell/core';
import { AccountServiceBase } from 'CodeShell/Security';
import { NoteType } from 'CodeShell/Helpers';

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



