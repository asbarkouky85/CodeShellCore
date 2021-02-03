import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { CodeShellModule } from "codeshell/codeShellModule";
import { SharedModule } from "../../Shared/SharedModule";
import { Registry } from "codeshell/core";
import { AuthFilter, ResourceActions } from "codeshell/security";

import { ServerConfigBase } from "codeshell/core";
import { TranslateService } from "@ngx-translate/core";
import { AuthModule } from "../AuthModule";
import { RoleList } from "./RoleList";
import { RoleEdit } from "./RoleEdit";


@NgModule({
    declarations: [RoleList,RoleEdit,],
    exports: [RoleList,RoleEdit,],
    imports: [
		SharedModule,
		AuthModule,
        RouterModule.forChild([
			﻿{ path : "RoleList", component : RoleList, canActivate: [AuthFilter], data: { name : "Roles__RoleList", navigate: false, resource:"", action: "allowAll", apps:null }},
			﻿{ path : "RoleEdit/:id", component : RoleEdit, canActivate: [AuthFilter], data: { name : "Roles__RoleEdit", navigate: false, resource:"", action: "allowAll", apps:null }},

		])
    ],
	entryComponents:[]
})
export class RolesModule {

    constructor(trans: TranslateService, conf: ServerConfigBase) {
        trans.setDefaultLang(conf.Locale);
        trans.use(conf.Locale);
    }
}

Registry.Register("Auth/Roles/RoleList", RoleList);
Registry.Register("Auth/Roles/RoleEdit", RoleEdit);
