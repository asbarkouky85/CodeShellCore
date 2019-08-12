import { Component } from "@angular/core";
import { Shell, ServerConfigBase } from "codeshell/core";

import { BaseComponent } from "codeshell/baseComponents";

@Component({ templateUrl: "./topBar.html", selector: "top-bar" })
export class topBar extends BaseComponent {
    GetPageId(): number {
        throw new Error("Method not implemented.");
    }
    get Config(): ServerConfigBase { return Shell.Injector.get(ServerConfigBase); }

    Logout() {
        Shell.Session.EndSession();
        this.Router.navigateByUrl("/Login");
    }
}