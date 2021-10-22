import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";

import { CodeShellModule, Registry, ServerConfigBase } from "codeshell/main";
import { AuthFilter, ResourceActions } from "codeshell/security";

import { SharedModule } from "../../shared/shared.module";
import { Culture } from "codeshell/localization/locale-data";

import { RoutingModule } from "../routing.module";
import { PageCustomization } from './components/page-customization.component';
import { CustomizeAppComponent } from './components/customize-app.component';


@NgModule({
    declarations: [PageCustomization,CustomizeAppComponent,],
    exports: [PageCustomization,CustomizeAppComponent,],
    imports: [
		SharedModule,
		RoutingModule,
        RouterModule.forChild([
			﻿{ path : "page-customization/:id", component : PageCustomization, canActivate: [AuthFilter], data: { name : "PageControls__PageCustomization", navigate: false, resource:"PageControls", action: "anonymous", apps:null }},
			﻿{ path : "customize-app-component", component : CustomizeAppComponent, canActivate: [AuthFilter], data: { name : "PageControls__CustomizeAppComponent", navigate: false, resource:"", action: "allowAll", apps:null }},

		])
    ],
	entryComponents:[]
})
export class PageControlsModule {
	constructor(srv: TranslateService) {
		srv.use(Culture.Current.Language);
		srv.setDefaultLang(Culture.Current.Language);
	}
}

Registry.Register("Routing/PageControls/PageCustomization", PageCustomization);
Registry.Register("Routing/PageControls/CustomizeAppComponent", CustomizeAppComponent);
