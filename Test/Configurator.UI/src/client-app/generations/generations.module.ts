import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";

import { CodeShellModule, Registry, ServerConfigBase } from "codeshell/main";
import { AuthFilter, ResourceActions } from "codeshell/security";

import { SharedModule } from "../shared/shared.module";
import { Culture } from "codeshell/localization/locale-data";

import { GenerationInofList } from './components/generation-inof-list.component';
import { PageCategorySelect } from './components/page-category-select.component';


@NgModule({
    declarations: [GenerationInofList,PageCategorySelect,],
    exports: [GenerationInofList,PageCategorySelect,],
    imports: [
		SharedModule,
		
        RouterModule.forChild([
    { 
        path: 'development', 
        loadChildren: () => import('./development/development.module').then(m => m.DevelopmentModule) 
    },    { 
        path: 'tenants', 
        loadChildren: () => import('./tenants/tenants.module').then(m => m.TenantsModule) 
    },    { 
        path: 'environments', 
        loadChildren: () => import('./environments/environments.module').then(m => m.EnvironmentsModule) 
    },
		])
    ],
	entryComponents:[GenerationInofList, PageCategorySelect, ]
})
export class GenerationsModule {
	constructor(srv: TranslateService) {
		srv.use(Culture.Current.Language);
		srv.setDefaultLang(Culture.Current.Language);
	}
}

Registry.Register("Generations/GenerationInofList", GenerationInofList);
Registry.Register("Generations/PageCategorySelect", PageCategorySelect);
