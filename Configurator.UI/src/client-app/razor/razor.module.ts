import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";

import { CodeShellModule, Registry, ServerConfigBase } from "codeshell/main";
import { AuthFilter, ResourceActions } from "codeshell/security";

import { SharedModule } from "../shared/shared.module";
import { Culture } from "codeshell/localization/locale-data";

import { PageCategoryList } from './components/page-category-list.component';
import { PageCategoryCreate } from './components/page-category-create.component';
import { ModuleConfigModal } from './components/module-config-modal.component';
import { ServerTracerEmbed } from './components/server-tracer-embed.component';


@NgModule({
    declarations: [PageCategoryList,PageCategoryCreate,ModuleConfigModal,ServerTracerEmbed,],
    exports: [PageCategoryList,PageCategoryCreate,ModuleConfigModal,ServerTracerEmbed,],
    imports: [
		SharedModule,
		
        RouterModule.forChild([
    { 
        path: 'templates', 
        loadChildren: () => import('./templates/templates.module').then(m => m.TemplatesModule) 
    },
		])
    ],
	entryComponents:[PageCategoryList, PageCategoryCreate, ModuleConfigModal, ServerTracerEmbed, ]
})
export class RazorModule {
	constructor(srv: TranslateService) {
		srv.use(Culture.Current.Language);
		srv.setDefaultLang(Culture.Current.Language);
	}
}

Registry.Register("Razor/PageCategoryList", PageCategoryList);
Registry.Register("Razor/PageCategoryCreate", PageCategoryCreate);
Registry.Register("Razor/ModuleConfigModal", ModuleConfigModal);
Registry.Register("Razor/ServerTracerEmbed", ServerTracerEmbed);
