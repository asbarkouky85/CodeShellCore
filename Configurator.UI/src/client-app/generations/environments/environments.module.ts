import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";

import { CodeShellModule, Registry, ServerConfigBase } from "codeshell/main";
import { AuthFilter, ResourceActions } from "codeshell/security";

import { SharedModule } from "../../shared/shared.module";
import { Culture } from "codeshell/localization/locale-data";

import { GenerationsModule } from "../generations.module";
import { EnvironmentList } from './components/environment-list.component';
import { EnvironmentEditEmbedded } from './components/environment-edit-embedded.component';


@NgModule({
    declarations: [EnvironmentList,EnvironmentEditEmbedded,],
    exports: [EnvironmentList,EnvironmentEditEmbedded,],
    imports: [
		SharedModule,
		GenerationsModule,
        RouterModule.forChild([
			﻿{ path : "environment-list", component : EnvironmentList, canActivate: [AuthFilter], data: { name : "Environments__EnvironmentList", navigate: false, resource:"", action: "allowAll", apps:null }},

		])
    ],
	entryComponents:[EnvironmentEditEmbedded, ]
})
export class EnvironmentsModule {
	constructor(srv: TranslateService) {
		srv.use(Culture.Current.Language);
		srv.setDefaultLang(Culture.Current.Language);
	}
}

Registry.Register("Generations/Environments/EnvironmentList", EnvironmentList);
Registry.Register("Generations/Environments/EnvironmentEditEmbedded", EnvironmentEditEmbedded);
