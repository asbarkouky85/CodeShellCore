import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'CodeShell/BaseComponents';
import { Shell } from 'CodeShell/Shell';
import { IAccountService } from 'CodeShell/Security/IAccountService';
import { NoteType } from 'CodeShell/Helpers';

@Component({ templateUrl: "./Login.html" })
export class Login extends BaseComponent {
    loginData = {
        userName: '',
        password: ''
    };

    AccountService: IAccountService = Shell.Injector.get(IAccountService);

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



