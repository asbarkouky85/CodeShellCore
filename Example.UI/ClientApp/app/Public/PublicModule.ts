import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { CodeShellModule } from "codeshell/codeShellModule";
import { SharedModule } from "../Shared/SharedModule";
import { Registry } from "codeshell/core";
import { AuthFilter, ResourceActions } from "codeshell/security";

import { ServerConfigBase } from "codeshell/core";
import { TranslateService } from "@ngx-translate/core";
import { ForgotPassword } from "./ForgotPassword";


@NgModule({
    declarations: [ForgotPassword,],
    exports: [ForgotPassword,],
    imports: [
		SharedModule,
		
        RouterModule.forChild([
			﻿{ path : "ForgotPassword", component : ForgotPassword, canActivate: [AuthFilter], data: { name : "Public__ForgotPassword", navigate: false, resource:"", action: "anonymous", apps:null }},

		])
    ],
	entryComponents:[]
})
export class PublicModule {

    constructor(trans: TranslateService, conf: ServerConfigBase) {
        trans.setDefaultLang(conf.Locale);
        trans.use(conf.Locale);
    }
}

Registry.Register("Public/ForgotPassword", ForgotPassword);
