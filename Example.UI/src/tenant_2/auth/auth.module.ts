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
        path: 'users', 
        loadChildren: () => import('./users/users.module').then(m => m.UsersModule) 
    },
		])
    ],
	entryComponents:[]
})
export class AuthModule {
	constructor(srv: TranslateService) {
		srv.use(Culture.Current.Language);
		srv.setDefaultLang(Culture.Current.Language);
	}
}

