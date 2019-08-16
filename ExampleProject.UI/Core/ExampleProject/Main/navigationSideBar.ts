import { Component } from "@angular/core";
import { Shell } from "codeshell/core";

import { BaseComponent } from "codeshell/baseComponents";

@Component({ templateUrl: "./navigationSideBar.html", selector: "navigationSideBar" })
export class navigationSideBar extends BaseComponent {

    GetPageId(): number {
        return 0;
    }

    isLoggedIn: boolean = false;

    ngOnInit() {
        super.ngOnInit();
        this.isLoggedIn = Shell.Session.IsLoggedIn;
        Shell.Session.LogStatus.subscribe((v: boolean) => {
            this.isLoggedIn = v
        });
    }

    Logout() {

        Shell.Session.EndSession();
        this.Router.navigateByUrl("/Login");

    }
}