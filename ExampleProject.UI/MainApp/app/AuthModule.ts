import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { CodeShellModule } from "CodeShell/CodeShellModule";
import { SharedModule } from "./SharedModule";
import { Registry } from "CodeShell/Utilities/Registry";
import { AuthFilter } from "CodeShell/Security/AuthFilter";
import { ResourceActions } from "CodeShell/Security/Models";
import { IServerConfig } from "CodeShell/IServerConfig";
import { TranslateService } from "@ngx-translate/core";
import { UserCreate } from "./Auth/Users/UserCreate";


@NgModule({
    declarations: [UserCreate,],
    exports: [UserCreate,],
    imports: [
        CodeShellModule,
		SharedModule,
        RouterModule.forChild([
			﻿{ path : "UserCreate", component : UserCreate, canActivate: [AuthFilter], data: { name : "Auth__UserCreate", navigate: false, resource:"Auth__Users", action: ResourceActions.insert, apps:["Public"] }},

		])
    ]
})
export class AuthModule {

    constructor(trans: TranslateService, conf: IServerConfig) {
        trans.setDefaultLang(conf.Locale);
        trans.use(conf.Locale);
    }
}

Registry.Register("Auth/Users/UserCreate", UserCreate);
