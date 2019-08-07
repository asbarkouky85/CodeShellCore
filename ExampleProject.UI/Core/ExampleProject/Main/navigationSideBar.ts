import { Component } from "@angular/core";
import { Shell } from "CodeShell/Shell";
import { IServerConfig } from "CodeShell/IServerConfig";
import { BaseComponent } from "CodeShell/BaseComponents";

@Component({ templateUrl: "./navigationSideBar.html", selector: "navigationSideBar" })
export class navigationSideBar extends BaseComponent {
    GetPageId(): number {
        throw new Error("Method not implemented.");
    }
    get Config(): IServerConfig { return Shell.Injector.get(IServerConfig); }

    Logout() {
        debugger;
        Shell.Session.EndSession();
        this.Router.navigateByUrl("/Login");

    }
}