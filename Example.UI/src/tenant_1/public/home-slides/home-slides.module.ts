import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";

import { CodeShellModule, Registry, ServerConfigBase } from "codeshell/main";
import { AuthFilter, ResourceActions } from "codeshell/security";

import { SharedModule } from "../../shared/shared.module";
import { Culture } from "codeshell/localization/locale-data";

import { publicModule } from "../public.module";
import { HomeSlideList } from './components/home-slide-list.component';
import { HomeSlideCreate } from './components/home-slide-create.component';
import { HomeSlideEdit } from './components/home-slide-edit.component';


@NgModule({
    declarations: [HomeSlideList,HomeSlideCreate,HomeSlideEdit,],
    exports: [HomeSlideList,HomeSlideCreate,HomeSlideEdit,],
    imports: [
		SharedModule,
		publicModule,
        RouterModule.forChild([
			﻿{ path : "home-slide-list", component : HomeSlideList, canActivate: [AuthFilter], data: { name : "HomeSlides__HomeSlideList", navigate: false, resource:"", action: "allowAll", apps:null }},
			﻿{ path : "home-slide-create", component : HomeSlideCreate, canActivate: [AuthFilter], data: { name : "HomeSlides__HomeSlideCreate", navigate: false, resource:"", action: "allowAll", apps:null }},
			﻿{ path : "home-slide-edit/:id", component : HomeSlideEdit, canActivate: [AuthFilter], data: { name : "HomeSlides__HomeSlideEdit", navigate: false, resource:"", action: "allowAll", apps:null }},

		])
    ],
	entryComponents:[]
})
export class HomeSlidesModule {
	constructor(srv: TranslateService) {
		srv.use(Culture.Current.Language);
		srv.setDefaultLang(Culture.Current.Language);
	}
}

Registry.Register("public/HomeSlides/HomeSlideList", HomeSlideList);
Registry.Register("public/HomeSlides/HomeSlideCreate", HomeSlideCreate);
Registry.Register("public/HomeSlides/HomeSlideEdit", HomeSlideEdit);
