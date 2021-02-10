import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { CodeShellModule } from "codeshell/codeShellModule";
import { SharedModule } from "../Shared/SharedModule";
import { Registry } from "codeshell/core";
import { AuthFilter, ResourceActions } from "codeshell/security";

import { ServerConfigBase } from "codeshell/core";
import { TranslateService } from "@ngx-translate/core";
import { GenerationNotification } from "./GenerationNotification";


@NgModule({
    declarations: [GenerationNotification,],
    exports: [GenerationNotification,],
    imports: [
		SharedModule,
        RouterModule.forChild([
			﻿{ path : "GenerationNotification", component : GenerationNotification, canActivate: [AuthFilter], data: { name : "Generations__GenerationNotification", navigate: false, resource:"Generations", action: "anonymous", apps:null }},

		])
    ]
})
export class GenerationsModule {

    constructor(trans: TranslateService, conf: ServerConfigBase) {
        trans.setDefaultLang(conf.Locale);
        trans.use(conf.Locale);
    }
}

Registry.Register("Generations/GenerationNotification", GenerationNotification);
