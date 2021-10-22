import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";

import { CodeShellModule, Registry, ServerConfigBase } from "codeshell/main";
import { AuthFilter, ResourceActions } from "codeshell/security";

import { SharedModule } from "../../shared/shared.module";
import { Culture } from "codeshell/localization/locale-data";

import { RoutingModule } from "../routing.module";
import { PageTreeList } from './components/page-tree-list.component';
import { PageCreate } from './components/page-create.component';
import { PageEdit } from './components/page-edit.component';


@NgModule({
    declarations: [PageTreeList,PageCreate,PageEdit,],
    exports: [PageTreeList,PageCreate,PageEdit,],
    imports: [
		SharedModule,
		RoutingModule,
        RouterModule.forChild([
			﻿{ path : "page-tree-list", component : PageTreeList, canActivate: [AuthFilter], data: { name : "Pages__PageTreeList", navigate: false, resource:"Pages", action: "anonymous", apps:null }},
			﻿{ path : "page-create", component : PageCreate, canActivate: [AuthFilter], data: { name : "Pages__PageCreate", navigate: false, resource:"", action: "anonymous", apps:null }},
			﻿{ path : "page-edit/:id", component : PageEdit, canActivate: [AuthFilter], data: { name : "Pages__PageEdit", navigate: false, resource:"", action: "anonymous", apps:null }},

		])
    ],
	entryComponents:[]
})
export class PagesModule {
	constructor(srv: TranslateService) {
		srv.use(Culture.Current.Language);
		srv.setDefaultLang(Culture.Current.Language);
	}
}

Registry.Register("Routing/Pages/PageTreeList", PageTreeList);
Registry.Register("Routing/Pages/PageCreate", PageCreate);
Registry.Register("Routing/Pages/PageEdit", PageEdit);
