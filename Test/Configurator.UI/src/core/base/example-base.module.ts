import { ModuleWithProviders } from "@angular/compiler/src/core";
import { NgModule } from "@angular/core";
import { CodeShellModule, ServerConfigBase } from "codeshell/main";
import { TokenStorage, SessionTokenData } from "codeshell/security";
import { AccountServiceBase } from 'codeshell/http';

import { AccountService } from "./http/account.service";
import { Login } from "./main/login.component";
import { TopBar } from "./main/top-bar.component";
import { NavigationSideBar } from "./main/navigation-side-bar.component";
import { ServerConfig } from "./server-config";

@NgModule({
    declarations: [Login, TopBar, NavigationSideBar],

    imports: [
        CodeShellModule.forRoot(),
    ],
    exports: [
        CodeShellModule,
        Login, TopBar, NavigationSideBar
    ]
})
@NgModule({
    declarations: [Login, NavigationSideBar, TopBar],
    imports: [
        CodeShellModule
    ],
    exports: [
        CodeShellModule,
        Login, NavigationSideBar, TopBar
    ]
})
export class ExampleBaseModule {
    static forRoot(): ModuleWithProviders {
        return {
            ngModule: ExampleBaseModule,
            providers: [
                { provide: ServerConfigBase, useClass: ServerConfig },
                { provide: AccountServiceBase, useClass: AccountService }
            ]
        }
    }
}