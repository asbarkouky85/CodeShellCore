import { ModuleWithProviders } from "@angular/compiler/src/core";
import { NgModule } from "@angular/core";
import { CodeShellModule } from "codeshell/codeShellModule";
import { AccountServiceBase } from "codeshell/security";
import { Login } from "./Main/Login";
import { TopBar } from "./Main/topBar";
import { NavigationSideBar } from "./Main/navigationSideBar";
import { ServerConfigBase } from "codeshell/core";
import { ServerConfig } from "./ServerConfig";
import { AccountService } from "./Http/AccountService";
import { TokenStorage } from "codeshell/security/tokenStorage";
import { SessionTokenData } from "codeshell/security/sessionTokenData";

@NgModule({
    declarations: [Login, TopBar, NavigationSideBar],

    imports: [
        CodeShellModule,
    ],
    exports: [
        CodeShellModule,
        Login,
        TopBar,
        NavigationSideBar
    ]
})
export class ExampleBaseModule {
    static forRoot(): ModuleWithProviders {
        return {
            ngModule: ExampleBaseModule,
            providers: [
                { provide: ServerConfigBase, useClass: ServerConfig },
                { provide: AccountServiceBase, useClass: AccountService },
                { provide: TokenStorage, useClass: SessionTokenData }
            ]
        }
    }
}