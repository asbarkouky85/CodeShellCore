import { BaseComponent } from "./baseComponent";
import { AccountServiceBase } from "codeshell/security";
import { Shell } from "codeshell/core";
import { Utils } from "codeshell/helpers";

export class LoginBase extends BaseComponent{
    AccountService: AccountServiceBase = Shell.Injector.get(AccountServiceBase);
    model: any = {};

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