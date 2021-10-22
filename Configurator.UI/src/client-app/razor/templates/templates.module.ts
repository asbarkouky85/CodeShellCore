import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";

import { CodeShellModule, Registry, ServerConfigBase } from "codeshell/main";
import { AuthFilter, ResourceActions } from "codeshell/security";

import { SharedModule } from "../../shared/shared.module";
import { Culture } from "codeshell/localization/locale-data";

import { RazorModule } from "../razor.module";
import { PageCategoriesTreeList } from './components/page-categories-tree-list.component';
import { PageCategoryEdit } from './components/page-category-edit.component';


@NgModule({
    declarations: [PageCategoriesTreeList,PageCategoryEdit,],
    exports: [PageCategoriesTreeList,PageCategoryEdit,],
    imports: [
		SharedModule,
		RazorModule,
        RouterModule.forChild([
			﻿{ path : "page-categories-tree-list", component : PageCategoriesTreeList, canActivate: [AuthFilter], data: { name : "Templates__PageCategoriesTreeList", navigate: false, resource:"PageCategoriesTreeList", action: "anonymous", apps:null }},
			﻿{ path : "page-category-edit/:id", component : PageCategoryEdit, canActivate: [AuthFilter], data: { name : "Templates__PageCategoryEdit", navigate: false, resource:"", action: "allowAll", apps:null }},

		])
    ],
	entryComponents:[]
})
export class TemplatesModule {
	constructor(srv: TranslateService) {
		srv.use(Culture.Current.Language);
		srv.setDefaultLang(Culture.Current.Language);
	}
}

Registry.Register("Razor/Templates/PageCategoriesTreeList", PageCategoriesTreeList);
Registry.Register("Razor/Templates/PageCategoryEdit", PageCategoryEdit);
