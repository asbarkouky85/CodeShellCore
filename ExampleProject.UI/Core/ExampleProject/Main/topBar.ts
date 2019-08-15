import { Component } from "@angular/core";
import { Shell, ServerConfigBase } from "codeshell/core";

import { BaseComponent } from "codeshell/baseComponents";
import { UserDTO } from "codeshell/security";

@Component({ templateUrl: "./topBar.html", selector: "top-bar" })
export class topBar extends BaseComponent {
    GetPageId(): number {
        throw new Error("Method not implemented.");
    }
    isLoggedIn: boolean = false;

    ngOnInit() {
        this.isLoggedIn = Shell.Session.IsLoggedIn;
        Shell.Session.LogStatus.subscribe((v: boolean) => this.isLoggedIn = v);
    }

    Logout() {
        Shell.Session.EndSession();
        this.isLoggedIn = false;
        this.Router.navigateByUrl("/Login");
    }

    ChangeLang() { }
}