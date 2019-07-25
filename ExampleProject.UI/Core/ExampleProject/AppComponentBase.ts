import { Injector } from "@angular/core";
import { Title } from "@angular/platform-browser";
import { IServerConfig } from "CodeShell/IServerConfig";
import { IAppComponent } from "CodeShell/IAppComponent";
import { TestLoader } from "CodeShell/Directives";

export class AppComponentBase extends IAppComponent {
    ModalLoader?: TestLoader | undefined;
    constructor(inj: Injector, trans: Title, conf: IServerConfig) {
        super(inj, trans, conf);
    }
}