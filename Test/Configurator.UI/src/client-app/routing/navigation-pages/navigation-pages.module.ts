import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";

import { CodeShellModule, Registry, ServerConfigBase } from "codeshell/main";
import { AuthFilter, ResourceActions } from "codeshell/security";

import { SharedModule } from "../../shared/shared.module";
import { Culture } from "codeshell/localization/locale-data";

import { RoutingModule } from "../routing.module";
import { NavGroupPages } from './components/nav-group-pages.component';


@NgModule({
    declarations: [NavGroupPages,],
    exports: [NavGroupPages,],
    imports: [
		SharedModule,
		RoutingModule,
        RouterModule.forChild([
			﻿{ path : "nav-group-pages", component : NavGroupPages, canActivate: [AuthFilter], data: { name : "NavigationPages__NavGroupPages", navigate: false, resource:"NavigationGroups", action: "anonymous", apps:null }},

		])
    ],
	entryComponents:[]
})
export class NavigationPagesModule {
	constructor(srv: TranslateService) {
		srv.use(Culture.Current.Language);
		srv.setDefaultLang(Culture.Current.Language);
	}
}

Registry.Register("Routing/NavigationPages/NavGroupPages", NavGroupPages);
