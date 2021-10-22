import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";

import { CodeShellModule, Registry, ServerConfigBase } from "codeshell/main";
import { AuthFilter, ResourceActions } from "codeshell/security";

import { SharedModule } from "../../shared/shared.module";
import { Culture } from "codeshell/localization/locale-data";

import { LocalizationModule } from "../localization.module";
import { CustomTextList } from './components/custom-text-list.component';


@NgModule({
    declarations: [CustomTextList,],
    exports: [CustomTextList,],
    imports: [
		SharedModule,
		LocalizationModule,
        RouterModule.forChild([
			﻿{ path : "custom-text-list", component : CustomTextList, canActivate: [AuthFilter], data: { name : "CustomTexts__CustomTextList", navigate: false, resource:"", action: "allowAll", apps:null }},

		])
    ],
	entryComponents:[]
})
export class CustomTextsModule {
	constructor(srv: TranslateService) {
		srv.use(Culture.Current.Language);
		srv.setDefaultLang(Culture.Current.Language);
	}
}

Registry.Register("Localization/CustomTexts/CustomTextList", CustomTextList);
