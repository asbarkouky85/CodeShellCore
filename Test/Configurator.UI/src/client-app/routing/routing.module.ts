import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";

import { CodeShellModule, Registry, ServerConfigBase } from "codeshell/main";
import { AuthFilter, ResourceActions } from "codeshell/security";

import { SharedModule } from "../shared/shared.module";
import { Culture } from "codeshell/localization/locale-data";

import { PageList } from './components/page-list.component';
import { NaveList } from './components/nave-list.component';
import { NavigationPageList } from './components/navigation-page-list.component';
import { NavigationPageCreate } from './components/navigation-page-create.component';


@NgModule({
    declarations: [PageList,NaveList,NavigationPageList,NavigationPageCreate,],
    exports: [PageList,NaveList,NavigationPageList,NavigationPageCreate,],
    imports: [
		SharedModule,
		
        RouterModule.forChild([
    { 
        path: 'pages', 
        loadChildren: () => import('./pages/pages.module').then(m => m.PagesModule) 
    },    { 
        path: 'navigation-pages', 
        loadChildren: () => import('./navigation-pages/navigation-pages.module').then(m => m.NavigationPagesModule) 
    },    { 
        path: 'page-controls', 
        loadChildren: () => import('./page-controls/page-controls.module').then(m => m.PageControlsModule) 
    },    { 
        path: 'parameters', 
        loadChildren: () => import('./parameters/parameters.module').then(m => m.ParametersModule) 
    },
		])
    ],
	entryComponents:[PageList, NaveList, NavigationPageList, NavigationPageCreate, ]
})
export class RoutingModule {
	constructor(srv: TranslateService) {
		srv.use(Culture.Current.Language);
		srv.setDefaultLang(Culture.Current.Language);
	}
}

Registry.Register("Routing/PageList", PageList);
Registry.Register("Routing/NaveList", NaveList);
Registry.Register("Routing/NavigationPageList", NavigationPageList);
Registry.Register("Routing/NavigationPageCreate", NavigationPageCreate);
