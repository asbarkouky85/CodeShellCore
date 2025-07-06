import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";

import { CodeShellModule, Registry, ServerConfigBase } from "codeshell/main";
import { AuthFilter, ResourceActions } from "codeshell/security";

import { SharedModule } from "../../shared/shared.module";
import { Culture } from "codeshell/localization/locale-data";

import { GenerationsModule } from "../generations.module";
import { TenantList } from './components/tenant-list.component';
import { TenantCreate } from './components/tenant-create.component';
import { TenantDetails } from './components/tenant-details.component';


@NgModule({
    declarations: [TenantList,TenantCreate,TenantDetails,],
    exports: [TenantList,TenantCreate,TenantDetails,],
    imports: [
		SharedModule,
		GenerationsModule,
        RouterModule.forChild([
			﻿{ path : "tenant-list", component : TenantList, canActivate: [AuthFilter], data: { name : "Tenants__TenantList", navigate: false, resource:"", action: "anonymous", apps:null }},
			﻿{ path : "tenant-create", component : TenantCreate, canActivate: [AuthFilter], data: { name : "Tenants__TenantCreate", navigate: false, resource:"", action: "allowAll", apps:null }},
			﻿{ path : "tenant-details/:id", component : TenantDetails, canActivate: [AuthFilter], data: { name : "Tenants__TenantDetails", navigate: false, resource:"", action: "allowAll", apps:null }},

		])
    ],
	entryComponents:[]
})
export class TenantsModule {
	constructor(srv: TranslateService) {
		srv.use(Culture.Current.Language);
		srv.setDefaultLang(Culture.Current.Language);
	}
}

Registry.Register("Generations/Tenants/TenantList", TenantList);
Registry.Register("Generations/Tenants/TenantCreate", TenantCreate);
Registry.Register("Generations/Tenants/TenantDetails", TenantDetails);
