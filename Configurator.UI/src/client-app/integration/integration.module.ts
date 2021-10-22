import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";

import { CodeShellModule, Registry, ServerConfigBase } from "codeshell/main";
import { AuthFilter, ResourceActions } from "codeshell/security";

import { SharedModule } from "../shared/shared.module";
import { Culture } from "codeshell/localization/locale-data";

import { ResourceModal } from './components/resource-modal.component';
import { ResourceEditModal } from './components/resource-edit-modal.component';


@NgModule({
    declarations: [ResourceModal,ResourceEditModal,],
    exports: [ResourceModal,ResourceEditModal,],
    imports: [
		SharedModule,
		
        RouterModule.forChild([
    { 
        path: 'resources', 
        loadChildren: () => import('./resources/resources.module').then(m => m.ResourcesModule) 
    },
		])
    ],
	entryComponents:[ResourceModal, ResourceEditModal, ]
})
export class IntegrationModule {
	constructor(srv: TranslateService) {
		srv.use(Culture.Current.Language);
		srv.setDefaultLang(Culture.Current.Language);
	}
}

Registry.Register("Integration/ResourceModal", ResourceModal);
Registry.Register("Integration/ResourceEditModal", ResourceEditModal);
