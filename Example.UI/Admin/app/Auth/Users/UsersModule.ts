import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { CodeShellModule } from "codeshell/codeShellModule";
import { SharedModule } from "../../Shared/SharedModule";
import { Registry } from "codeshell/core";
import { AuthFilter, ResourceActions } from "codeshell/security";

import { ServerConfigBase } from "codeshell/core";
import { TranslateService } from "@ngx-translate/core";
import { AuthModule } from "../AuthModule";
import { UserCreate } from "./UserCreate";


@NgModule({
    declarations: [UserCreate,],
    exports: [UserCreate,],
    imports: [
		SharedModule,
		AuthModule,
        RouterModule.forChild([
			﻿{ path : "UserCreate", component : UserCreate, canActivate: [AuthFilter], data: { name : "Users__UserCreate", navigate: false, resource:"", action: "anonymous", apps:null }},

		])
    ],
	entryComponents:[]
})
export class UsersModule {

    constructor(trans: TranslateService, conf: ServerConfigBase) {
        trans.setDefaultLang(conf.Locale);
        trans.use(conf.Locale);
    }
}

Registry.Register("Auth/Users/UserCreate", UserCreate);
