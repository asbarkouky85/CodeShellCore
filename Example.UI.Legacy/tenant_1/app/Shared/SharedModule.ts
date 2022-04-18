import { NgModule } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { ExampleBaseModule } from 'Example/ExampleBaseModule';
import { ServerConfigBase, Registry } from "codeshell/core";


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

