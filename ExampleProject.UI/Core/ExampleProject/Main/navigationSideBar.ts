import { Component } from "@angular/core";
import { Shell, ServerConfigBase } from "codeshell/core";

import { BaseComponent } from "codeshell/baseComponents";

@Component({ templateUrl: "./navigationSideBar.html", selector: "navigationSideBar" })
export class navigationSideBar extends BaseComponent {
    GetPageId(): number {
        throw new Error("Method not implemented.");
    }
    get Config(): ServerConfigBase { return Shell.Injector.get(ServerConfigBase); }

    Logout() {
        debugger;
        Shell.Session.EndSession();
        this.Router.navigateByUrl("/Login");

    }
}