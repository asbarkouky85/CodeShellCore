import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { CodeShellModule } from "codeshell/codeShellModule";
import { SharedModule } from "../Shared/SharedModule";
import { Registry } from "codeshell/core";
import { AuthFilter, ResourceActions } from "codeshell/security";

import { ServerConfigBase } from "codeshell/core";
import { TranslateService } from "@ngx-translate/core";
import { TenantList } from "./TenantList";
import { TenantCreate } from "./TenantCreate";
import { TenantDetails } from "./TenantDetails";


@NgModule({
    declarations: [TenantList,TenantCreate,TenantDetails,],
    exports: [TenantList,TenantCreate,TenantDetails,],
    imports: [
		SharedModule,
        RouterModule.forChild([
			﻿{ path : "TenantList", component : TenantList, canActivate: [AuthFilter], data: { name : "Tenants__TenantList", navigate: false, resource:"", action: "anonymous", apps:null }},
			﻿{ path : "TenantCreate", component : TenantCreate, canActivate: [AuthFilter], data: { name : "Tenants__TenantCreate", navigate: false, resource:"", action: "allowAll", apps:null }},
			﻿{ path : "TenantDetails/:id", component : TenantDetails, canActivate: [AuthFilter], data: { name : "Tenants__TenantDetails", navigate: false, resource:"", action: "allowAll", apps:null }},

		])
    ]
})
export class TenantsModule {

    constructor(trans: TranslateService, conf: ServerConfigBase) {
        trans.setDefaultLang(conf.Locale);
        trans.use(conf.Locale);
    }
}

Registry.Register("Tenants/TenantList", TenantList);
Registry.Register("Tenants/TenantCreate", TenantCreate);
Registry.Register("Tenants/TenantDetails", TenantDetails);
