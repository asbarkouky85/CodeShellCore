import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { CodeShellModule } from "codeshell";
import { SharedModule } from "./SharedModule";
import { Registry } from "codeshell/core";
import { AuthFilter, ResourceActions } from "codeshell/security";

import { ServerConfigBase } from "codeshell/core";
import { TranslateService } from "@ngx-translate/core";
import { UserCreate } from "./Auth/Users/UserCreate";


@NgModule({
    declarations: [UserCreate,],
    exports: [UserCreate,],
    imports: [
        CodeShellModule,
		SharedModule,
        RouterModule.forChild([
			﻿{ path : "UserCreate", component : UserCreate, canActivate: [AuthFilter], data: { name : "Auth__UserCreate", navigate: true, resource:"Users", action: ResourceActions.insert, apps:null }},

		])
    ]
})
export class AuthModule {

    constructor(trans: TranslateService, conf: ServerConfigBase) {
        trans.setDefaultLang(conf.Locale);
        trans.use(conf.Locale);
    }
}

Registry.Register("Auth/Users/UserCreate", UserCreate);
