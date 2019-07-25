import { Injector } from "@angular/core";
import { Title } from "@angular/platform-browser";
import { ServerConfigBase } from "codeshell/core";
import { IAppComponent } from "CodeShell/baseComponents";
import { TestLoader } from "CodeShell/core";

export class AppComponentBase extends IAppComponent {
    ModalLoader?: TestLoader | undefined;
    constructor(inj: Injector, trans: Title, conf: ServerConfigBase) {
        super(inj, trans, conf);
    }
}