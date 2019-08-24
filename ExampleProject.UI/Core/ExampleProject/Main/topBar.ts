import { Component } from "@angular/core";
import { Shell, ServerConfigBase } from "codeshell/core";

import { BaseComponent } from "codeshell/baseComponents";
import { UserDTO } from "codeshell/security";
import { HttpClient } from "@angular/common/http";

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

    Slide() {
        $(".content-wrapper").toggleClass("expanded");
        $(".nav-wrapper").toggleClass("compressed");
    }

    ChangeLang() {
        var cl: HttpClient = Shell.Injector.get(HttpClient);
        var conf: ServerConfigBase = Shell.Injector.get(ServerConfigBase);
        cl.get("/Home/SetLocale/?lang=" + (conf.Locale=='ar'?'en':'ar')).subscribe(d => {
            location.reload();
        });
    }
}