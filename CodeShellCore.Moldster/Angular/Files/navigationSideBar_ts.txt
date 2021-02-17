import { Component, Injector } from "@angular/core";
import { NavigationSideBarBase } from "codeshell/base-components";

@Component({ templateUrl: "./navigation-side-bar.component.html", selector: "app-navigation-side-bar" })
export class NavigationSideBar extends NavigationSideBarBase {

    constructor(inj:Injector){
        super(inj);
    }
}