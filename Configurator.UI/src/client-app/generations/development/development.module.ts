import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";

import { CodeShellModule, Registry, ServerConfigBase } from "codeshell/main";
import { AuthFilter, ResourceActions } from "codeshell/security";

import { SharedModule } from "../../shared/shared.module";
import { Culture } from "codeshell/localization/locale-data";

import { GenerationsModule } from "../generations.module";
import { DevelopmentPanel } from './components/development-panel.component';


@NgModule({
    declarations: [DevelopmentPanel,],
    exports: [DevelopmentPanel,],
    imports: [
		SharedModule,
		GenerationsModule,
        RouterModule.forChild([
			﻿{ path : "development-panel", component : DevelopmentPanel, canActivate: [AuthFilter], data: { name : "Development__DevelopmentPanel", navigate: false, resource:"Generations", action: "anonymous", apps:null }},

		])
    ],
	entryComponents:[]
})
export class DevelopmentModule {
	constructor(srv: TranslateService) {
		srv.use(Culture.Current.Language);
		srv.setDefaultLang(Culture.Current.Language);
	}
}

Registry.Register("Generations/Development/DevelopmentPanel", DevelopmentPanel);
