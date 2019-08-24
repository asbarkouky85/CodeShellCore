import { ModuleWithProviders } from "@angular/compiler/src/core";
import { NgModule } from "@angular/core";
import { CodeShellModule } from "codeshell/index";
import { ServerConfigBase } from "codeshell/core";
import { AuthFilter } from "codeshell/security";
import { ServerConfig } from "./ServerConfig";
import { Login } from "./Main/Login";
import { topBar } from "./Main/topBar";
import { navigationSideBar } from "./Main/navigationSideBar";

@NgModule({
    declarations: [Login, topBar, navigationSideBar],

    imports: [
        CodeShellModule
    ],
    exports: [
        CodeShellModule,
        topBar, navigationSideBar
    ]
})
export class ExampleProjectBaseModule {
    static forRoot(): ModuleWithProviders {
        return {
            ngModule: ExampleProjectBaseModule,
            providers: [
                { provide: ServerConfigBase, useClass: ServerConfig },
                { provide: 'BASE_URL', useFactory: getBaseUrl },
            ]
        }
    }
}

var bs: string | null = null;

export function getBaseUrl() {
    if (bs == null)
        bs = document.getElementsByTagName('base')[0].href;
    return bs;
}