import { NgModule } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { ExampleBaseModule } from '@base/example-base.module';
import { ServerConfigBase, Registry } from "codeshell/main";


@NgModule({
    declarations: [],
    exports: [
		
		ExampleBaseModule
	],
    imports: [
        ExampleBaseModule
    ],
	entryComponents:[]
})
export class SharedModule {

	constructor(trans: TranslateService, conf: ServerConfigBase) {
        trans.setDefaultLang(conf.Locale);
        trans.use(conf.Locale);
    }
}

