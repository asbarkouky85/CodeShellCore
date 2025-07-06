import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";

import { CodeShellModule, Registry, ServerConfigBase } from "codeshell/main";
import { AuthFilter, ResourceActions } from "codeshell/security";

import { SharedModule } from "../shared/shared.module";
import { Culture } from "codeshell/localization/locale-data";



@NgModule({
    declarations: [],
    exports: [],
    imports: [
		SharedModule,
		
        RouterModule.forChild([
    { 
        path: 'custom-texts', 
        loadChildren: () => import('./custom-texts/custom-texts.module').then(m => m.CustomTextsModule) 
    },
		])
    ],
	entryComponents:[]
})
export class LocalizationModule {
	constructor(srv: TranslateService) {
		srv.use(Culture.Current.Language);
		srv.setDefaultLang(Culture.Current.Language);
	}
}

