import { ModuleWithProviders } from "@angular/compiler/src/core";
import { NgModule } from "@angular/core";
import { CodeShellModule } from "codeshell/codeShellModule";
import { ServerConfigBase } from "codeshell/core";
import { AuthFilter, AccountServiceBase } from "codeshell/security";
import { ServerConfig } from "./ServerConfig";
import { Login } from "./Main/Login";
import { NavigationSideBar } from "./Main/navigationSideBar";
import { TopBar } from "./Main/topBar";
import { TenantsService } from "./Http";
import { PagesService } from "./Http";
import { DomainsService } from "./Http";
import { PageCategoriesService } from "./Http";
import { NavigationGroupsService } from "./Http/NavigationGroupsService";
import { AccountService } from "./Http/AccountService";
import { PageControlsService } from "./Http/PageControlsService";
import { ResourcesService } from "./Http/ResourcesService";

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
                { provide: AccountServiceBase, useClass: AccountService },
                TenantsService,
                PagesService,
                DomainsService,
                PageCategoriesService,
                NavigationGroupsService,
                PageControlsService, ResourcesService
            ]
        }
    }
}