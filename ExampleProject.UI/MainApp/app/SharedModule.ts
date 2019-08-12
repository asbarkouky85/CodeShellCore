import { NgModule } from "@angular/core";
import { CodeShellModule } from "codeshell";
import { Registry } from "codeshell/core";
import { ServerConfig } from "ExampleProject/ServerConfig";
import { TranslateService } from "@ngx-translate/core";


@NgModule({
    declarations: [],
    exports: [],
    imports: [
        CodeShellModule
    ],
	entryComponents:[]
})
export class SharedModule {

    constructor(trans: TranslateService) {
        trans.setDefaultLang(ServerConfig.Instance.Locale);
        trans.use(ServerConfig.Instance.Locale);
    }
}

