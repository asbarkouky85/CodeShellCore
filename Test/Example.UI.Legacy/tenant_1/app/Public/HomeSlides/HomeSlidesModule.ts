import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { CodeShellModule } from "codeshell/codeShellModule";
import { SharedModule } from "../../Shared/SharedModule";
import { Registry } from "codeshell/core";
import { AuthFilter, ResourceActions } from "codeshell/security";

import { ServerConfigBase } from "codeshell/core";
import { TranslateService } from "@ngx-translate/core";
import { PublicModule } from "../PublicModule";
import { HomeSlideList } from './HomeSlideList';
import { HomeSlideCreate } from './HomeSlideCreate';
import { HomeSlideEdit } from './HomeSlideEdit';


@NgModule({
    declarations: [HomeSlideList,HomeSlideCreate,HomeSlideEdit,],
    exports: [HomeSlideList,HomeSlideCreate,HomeSlideEdit,],
    imports: [
		SharedModule,
		PublicModule,
        RouterModule.forChild([
			﻿{ path : "HomeSlideList", component : HomeSlideList, canActivate: [AuthFilter], data: { name : "HomeSlides__HomeSlideList", navigate: false, resource:"", action: "allowAll", apps:null }},
			﻿{ path : "HomeSlideCreate", component : HomeSlideCreate, canActivate: [AuthFilter], data: { name : "HomeSlides__HomeSlideCreate", navigate: false, resource:"", action: "allowAll", apps:null }},
			﻿{ path : "HomeSlideEdit/:id", component : HomeSlideEdit, canActivate: [AuthFilter], data: { name : "HomeSlides__HomeSlideEdit", navigate: false, resource:"", action: "allowAll", apps:null }},

		])
    ],
	entryComponents:[]
})
export class HomeSlidesModule {

    constructor(trans: TranslateService, conf: ServerConfigBase) {
        trans.setDefaultLang(conf.Locale);
        trans.use(conf.Locale);
    }
}

Registry.Register("public/HomeSlides/HomeSlideList", HomeSlideList);
Registry.Register("public/HomeSlides/HomeSlideCreate", HomeSlideCreate);
Registry.Register("public/HomeSlides/HomeSlideEdit", HomeSlideEdit);
