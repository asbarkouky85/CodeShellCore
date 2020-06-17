import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { CodeShellModule } from "codeshell/codeShellModule";
import { SharedModule } from "../Shared/SharedModule";
import { Registry } from "codeshell/core";
import { AuthFilter, ResourceActions } from "codeshell/security";

import { ServerConfigBase } from "codeshell/core";
import { TranslateService } from "@ngx-translate/core";
import { PageTreeList } from "./PageTreeList";
import { PageCreate } from "./PageCreate";
import { PageEdit } from "./PageEdit";


@NgModule({
    declarations: [PageTreeList,PageCreate,PageEdit,],
    exports: [PageTreeList,PageCreate,PageEdit,],
    imports: [
		SharedModule,
        RouterModule.forChild([
			﻿{ path : "PageTreeList", component : PageTreeList, canActivate: [AuthFilter], data: { name : "Pages__PageTreeList", navigate: false, resource:"Pages", action: "anonymous", apps:null }},
			﻿{ path : "PageCreate", component : PageCreate, canActivate: [AuthFilter], data: { name : "Pages__PageCreate", navigate: false, resource:"", action: "anonymous", apps:null }},
			﻿{ path : "PageEdit/:id", component : PageEdit, canActivate: [AuthFilter], data: { name : "Pages__PageEdit", navigate: false, resource:"", action: "anonymous", apps:null }},

		])
    ]
})
export class PagesModule {

    constructor(trans: TranslateService, conf: ServerConfigBase) {
        trans.setDefaultLang(conf.Locale);
        trans.use(conf.Locale);
    }
}

Registry.Register("Pages/PageTreeList", PageTreeList);
Registry.Register("Pages/PageCreate", PageCreate);
Registry.Register("Pages/PageEdit", PageEdit);
