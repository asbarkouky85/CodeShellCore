import { Component, EventEmitter } from "@angular/core";
import { Shell, ServerConfigBase } from "codeshell/core";

import { BaseComponent } from "codeshell/baseComponents";
import { UserDTO } from "codeshell/security";
import { Observable } from "rxjs";
import { of } from "rxjs/observable/of";
import value from "*.json";

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
        console.log("nav")
    }

    Logout() {

        Shell.Session.EndSession();
        this.Router.navigateByUrl("/Login");

    }
}