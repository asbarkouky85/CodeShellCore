import { ModuleWithProviders } from "@angular/compiler/src/core";
import { NgModule } from "@angular/core";
import { CodeShellModule } from "codeshell";
import { Login } from "./Main/Login";
import { topBar } from "./Main/topBar";
import { navigationSideBar } from "./Main/navigationSideBar";
import { ServerConfigBase } from "codeshell/core";
import { ServerConfig } from "./ServerConfig";
import { AccountServiceBase } from "codeshell/security";
import { AccountService } from "./Http/AccountService";
import { UsersService } from "./Auth/Users/Http";

@NgModule({
    declarations: [Login, topBar, navigationSideBar],

    imports: [
        CodeShellModule,
    ],
    exports: [
        CodeShellModule,
        Login,
        topBar,
        navigationSideBar
    ]
})
export class ExampleProjectBaseModule {
    static forRoot(): ModuleWithProviders {
        return {
            ngModule: ExampleProjectBaseModule,
            providers: [
                UsersService,
                { provide: ServerConfigBase, useClass: ServerConfig },
                { provide: AccountServiceBase, useClass: AccountService }
            ]
        }
    }
}