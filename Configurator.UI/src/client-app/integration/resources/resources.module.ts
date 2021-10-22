import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";

import { CodeShellModule, Registry, ServerConfigBase } from "codeshell/main";
import { AuthFilter, ResourceActions } from "codeshell/security";

import { SharedModule } from "../../shared/shared.module";
import { Culture } from "codeshell/localization/locale-data";

import { IntegrationModule } from "../integration.module";
import { ResourceList } from './components/resource-list.component';


@NgModule({
    declarations: [ResourceList,],
    exports: [ResourceList,],
    imports: [
		SharedModule,
		IntegrationModule,
        RouterModule.forChild([
			﻿{ path : "resource-list", component : ResourceList, canActivate: [AuthFilter], data: { name : "Resources__ResourceList", navigate: false, resource:"", action: "allowAll", apps:null }},

		])
    ],
	entryComponents:[]
})
export class ResourcesModule {
	constructor(srv: TranslateService) {
		srv.use(Culture.Current.Language);
		srv.setDefaultLang(Culture.Current.Language);
	}
}

Registry.Register("Integration/Resources/ResourceList", ResourceList);
