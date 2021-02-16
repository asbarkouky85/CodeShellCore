import { ModuleWithProviders } from "@angular/compiler/src/core";
import { NgModule } from "@angular/core";
import { CodeShellModule } from "codeshell/main";
import { AccountServiceBase, TokenStorage, SessionTokenData } from "codeshell/security";

import { AccountService } from "./http/account.service";
import { Login } from "./main/login.component";
import { TopBar } from "./main/top-bar.component";
import { NavigationSideBar } from "./main/navigation-side-bar.component";

@NgModule({
    declarations: [Login, TopBar, NavigationSideBar],

    imports: [
        CodeShellModule,
    ],
    exports: [
        CodeShellModule,
        Login, TopBar, NavigationSideBar
    ]
})
export class ExampleBaseModule {
    static forRoot(): ModuleWithProviders {
        return {
            ngModule: ExampleBaseModule,
            providers: [
                { provide: AccountServiceBase, useClass: AccountService },
                { provide: TokenStorage, useClass: SessionTokenData }
            ]
        }
    }
}