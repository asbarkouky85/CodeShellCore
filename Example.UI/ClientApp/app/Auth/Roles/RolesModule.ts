import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { CodeShellModule } from "codeshell/codeShellModule";
import { SharedModule } from "../../Shared/SharedModule";
import { Registry } from "codeshell/core";
import { AuthFilter, ResourceActions } from "codeshell/security";

import { ServerConfigBase } from "codeshell/core";
import { TranslateService } from "@ngx-translate/core";
import { RoleEdit } from "./RoleEdit";
import { RoleList } from "./RoleList";
import { RoleCreate } from "./RoleCreate";


@NgModule({
    declarations: [RoleEdit,RoleList,RoleCreate,],
    exports: [RoleEdit,RoleList,RoleCreate,],
    imports: [
		SharedModule,
        RouterModule.forChild([
			﻿{ path : "RoleEdit/:id", component : RoleEdit, canActivate: [AuthFilter], data: { name : "Roles__RoleEdit", navigate: false, resource:"", action: "allowAll", apps:null }},
			﻿{ path : "RoleList", component : RoleList, canActivate: [AuthFilter], data: { name : "Roles__RoleList", navigate: false, resource:"", action: "allowAll", apps:null }},
			﻿{ path : "RoleCreate", component : RoleCreate, canActivate: [AuthFilter], data: { name : "Roles__RoleCreate", navigate: false, resource:"", action: "allowAll", apps:null }},

		])
    ]
})
export class RolesModule {

    constructor(trans: TranslateService, conf: ServerConfigBase) {
        trans.setDefaultLang(conf.Locale);
        trans.use(conf.Locale);
    }
}

Registry.Register("Auth/Roles/RoleEdit", RoleEdit);
Registry.Register("Auth/Roles/RoleList", RoleList);
Registry.Register("Auth/Roles/RoleCreate", RoleCreate);
