import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";

import { CodeShellModule, Registry, ServerConfigBase } from "codeshell/main";
import { AuthFilter, ResourceActions } from "codeshell/security";

import { SharedModule } from "../../shared/shared.module";
import { Culture } from "codeshell/localization/locale-data";

import { RoutingModule } from "../routing.module";
import { PageReferenceList } from './components/page-reference-list.component';


@NgModule({
    declarations: [PageReferenceList,],
    exports: [PageReferenceList,],
    imports: [
		SharedModule,
		RoutingModule,
        RouterModule.forChild([
			﻿{ path : "page-reference-list", component : PageReferenceList, canActivate: [AuthFilter], data: { name : "Parameters__PageReferenceList", navigate: false, resource:"", action: "anonymous", apps:null }},

		])
    ],
	entryComponents:[]
})
export class ParametersModule {
	constructor(srv: TranslateService) {
		srv.use(Culture.Current.Language);
		srv.setDefaultLang(Culture.Current.Language);
	}
}

Registry.Register("Routing/Parameters/PageReferenceList", PageReferenceList);
