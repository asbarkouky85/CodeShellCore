import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";

import { CodeShellModule, Registry, ServerConfigBase } from "codeshell/main";
import { AuthFilter, ResourceActions } from "codeshell/security";

import { SharedModule } from "../../shared/shared.module";
import { Culture } from "codeshell/localization/locale-data";

import { AuthModule } from "../auth.module";
import { UserList } from './components/user-list.component';


@NgModule({
    declarations: [UserList,],
    exports: [UserList,],
    imports: [
		SharedModule,
		AuthModule,
        RouterModule.forChild([
			﻿{ path : "user-list", component : UserList, canActivate: [AuthFilter], data: { name : "Users__UserList", navigate: false, resource:"", action: "anonymous", apps:null }},

		])
    ],
	entryComponents:[]
})
export class UsersModule {
	constructor(srv: TranslateService) {
		srv.use(Culture.Current.Language);
		srv.setDefaultLang(Culture.Current.Language);
	}
}

Registry.Register("Auth/Users/UserList", UserList);
