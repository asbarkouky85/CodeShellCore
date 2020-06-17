import { NgModule } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { BaseAppBaseModule } from 'BaseApp/BaseAppBaseModule';
import { ServerConfigBase, Registry } from "codeshell/core";


@NgModule({
    declarations: [],
    exports: [
		
		BaseAppBaseModule
	],
    imports: [
        BaseAppBaseModule
    ],
	entryComponents:[]
})
export class SharedModule {

	constructor(trans: TranslateService, conf: ServerConfigBase) {
        trans.setDefaultLang(conf.Locale);
        trans.use(conf.Locale);
    }
}

