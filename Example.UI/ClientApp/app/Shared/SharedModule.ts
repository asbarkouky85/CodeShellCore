import { NgModule } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { BaseAppBaseModule } from 'BaseApp/BaseAppBaseModule';
import { ServerConfigBase, Registry } from "codeshell/core";
import { ChangePassword } from "./ChangePassword";


@NgModule({
    declarations: [ChangePassword,],
    exports: [
		ChangePassword,
		BaseAppBaseModule
	],
    imports: [
        BaseAppBaseModule
    ],
	entryComponents:[ChangePassword, ]
})
export class SharedModule {

	constructor(trans: TranslateService, conf: ServerConfigBase) {
        trans.setDefaultLang(conf.Locale);
        trans.use(conf.Locale);
    }
}

Registry.Register("Shared/ChangePassword", ChangePassword);
