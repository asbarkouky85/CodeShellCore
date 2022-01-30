import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { CodeShellModule } from "codeshell/codeShellModule";
import { SharedModule } from "../../Shared/SharedModule";
import { Registry } from "codeshell/core";
import { AuthFilter, ResourceActions } from "codeshell/security";

import { ServerConfigBase } from "codeshell/core";
import { TranslateService } from "@ngx-translate/core";
import { AuthModule } from "../AuthModule";
import { UserCreate } from "./UserCreate";
import { EditProfile } from "./EditProfile";
import { UserList } from "./UserList";
import { UserEdit } from "./UserEdit";


@NgModule({
    declarations: [UserCreate,EditProfile,UserList,UserEdit,],
    exports: [UserCreate,EditProfile,UserList,UserEdit,],
    imports: [
		SharedModule,
		AuthModule,
        RouterModule.forChild([
			﻿{ path : "UserCreate", component : UserCreate, canActivate: [AuthFilter], data: { name : "Users__UserCreate", navigate: false, resource:"", action: "allowAll", apps:null }},
			﻿{ path : "EditProfile", component : EditProfile, canActivate: [AuthFilter], data: { name : "Users__EditProfile", navigate: false, resource:"", action: "allowAll", apps:null }},
			﻿{ path : "UserList", component : UserList, canActivate: [AuthFilter], data: { name : "Users__UserList", navigate: false, resource:"", action: "allowAll", apps:null }},
			﻿{ path : "UserEdit/:id", component : UserEdit, canActivate: [AuthFilter], data: { name : "Users__UserEdit", navigate: false, resource:"", action: "allowAll", apps:null }},

		])
    ],
	entryComponents:[]
})
export class UsersModule {

    constructor(trans: TranslateService, conf: ServerConfigBase) {
        trans.setDefaultLang(conf.Locale);
        trans.use(conf.Locale);
    }
}

Registry.Register("Auth/Users/UserCreate", UserCreate);
Registry.Register("Auth/Users/EditProfile", EditProfile);
Registry.Register("Auth/Users/UserList", UserList);
Registry.Register("Auth/Users/UserEdit", UserEdit);
