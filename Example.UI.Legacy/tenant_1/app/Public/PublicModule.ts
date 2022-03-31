﻿import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { CodeShellModule } from "codeshell/codeShellModule";
import { SharedModule } from "../Shared/SharedModule";
import { Registry } from "codeshell/core";
import { AuthFilter, ResourceActions } from "codeshell/security";

import { ServerConfigBase } from "codeshell/core";
import { TranslateService } from "@ngx-translate/core";


@NgModule({
    declarations: [],
    exports: [],
    imports: [
		SharedModule,
		
        RouterModule.forChild([
{ path:"HomeSlides", loadChildren:"./HomeSlides/HomeSlidesModule#HomeSlidesModule" },
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
