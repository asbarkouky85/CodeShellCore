import { Component, Injector } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { Shell } from "codeshell";
import { BaseComponent } from "codeshell/base-components";
import { AccountServiceBase } from "codeshell/http";
import { NoteType } from "codeshell/results";
import { SessionManager } from "codeshell/security";

@Component({ templateUrl: "./login.component.html" })
export class Login extends BaseComponent {

    AccountService: AccountServiceBase = Shell.Injector.get(AccountServiceBase);
    model: any = {};
    GetPageId(): number { return 0; }

    constructor(route: ActivatedRoute, inj: Injector) {
        super(route, inj);
        
    }

    ngOnInit(){
        
    }

    Login() {
        this.AccountService.Login(this.model).then(data => {
            SessionManager.Current.StartSession(data);
            if (data.userData.tenantId == 2)
                location.pathname = "/"
            else
                this.Router.navigateByUrl("/");
        }).catch(error => {
            this.NotifyTranslate("invalid_credentials", NoteType.Error)
        })
    }
}