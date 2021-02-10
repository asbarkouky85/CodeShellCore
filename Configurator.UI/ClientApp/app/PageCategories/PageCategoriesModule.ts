import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { CodeShellModule } from "codeshell/codeShellModule";
import { SharedModule } from "../Shared/SharedModule";
import { Registry } from "codeshell/core";
import { AuthFilter, ResourceActions } from "codeshell/security";

import { ServerConfigBase } from "codeshell/core";
import { TranslateService } from "@ngx-translate/core";
import { PageCategoriesTreeList } from "./PageCategoriesTreeList";
import { PageCategoryEdit } from "./PageCategoryEdit";


@NgModule({
    declarations: [PageCategoriesTreeList,PageCategoryEdit,],
    exports: [PageCategoriesTreeList,PageCategoryEdit,],
    imports: [
		SharedModule,
        RouterModule.forChild([
			﻿{ path : "PageCategoriesTreeList", component : PageCategoriesTreeList, canActivate: [AuthFilter], data: { name : "PageCategories__PageCategoriesTreeList", navigate: false, resource:"PageCategoriesTreeList", action: "anonymous", apps:null }},
			﻿{ path : "PageCategoryEdit/:id", component : PageCategoryEdit, canActivate: [AuthFilter], data: { name : "PageCategories__PageCategoryEdit", navigate: false, resource:"", action: "allowAll", apps:null }},

		])
    ]
})
export class PageCategoriesModule {

    constructor(trans: TranslateService, conf: ServerConfigBase) {
        trans.setDefaultLang(conf.Locale);
        trans.use(conf.Locale);
    }
}

Registry.Register("PageCategories/PageCategoriesTreeList", PageCategoriesTreeList);
Registry.Register("PageCategories/PageCategoryEdit", PageCategoryEdit);
