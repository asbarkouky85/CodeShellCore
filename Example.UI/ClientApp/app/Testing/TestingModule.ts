import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { CodeShellModule } from "codeshell/codeShellModule";
import { SharedModule } from "../Shared/SharedModule";
import { Registry } from "codeshell/core";
import { AuthFilter, ResourceActions } from "codeshell/security";

import { ServerConfigBase } from "codeshell/core";
import { TranslateService } from "@ngx-translate/core";
import { TestPage } from "./TestPage";


@NgModule({
    declarations: [TestPage,],
    exports: [TestPage,],
    imports: [
		SharedModule,
        RouterModule.forChild([
			﻿{ path : "TestPage", component : TestPage, canActivate: [AuthFilter], data: { name : "Testing__TestPage", navigate: false, resource:"", action: "allowAll", apps:null }},

		])
    ]
})
export class TestingModule {

    constructor(trans: TranslateService, conf: ServerConfigBase) {
        trans.setDefaultLang(conf.Locale);
        trans.use(conf.Locale);
    }
}

Registry.Register("Testing/TestPage", TestPage);
