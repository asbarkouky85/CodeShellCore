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
        path: 'home-slides', 
        loadChildren: () => import('./home-slides/home-slides.module').then(m => m.HomeSlidesModule) 
    },
		])
    ],
	entryComponents:[]
})
export class PublicModule {
	constructor(srv: TranslateService) {
		srv.use(Culture.Current.Language);
		srv.setDefaultLang(Culture.Current.Language);
	}
}

