import { ModuleWithProviders } from "@angular/compiler/src/core";
import { NgModule } from "@angular/core";
import { CodeShellModule } from "codeshell";
import { BrowserModule } from "@angular/platform-browser";
import { ToastrModule } from "ngx-toastr";
import { TranslateModule } from "@ngx-translate/core";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations"
import { Login } from "./Main/Login";
import { topBar } from "./Main/topBar";
import { navigationSideBar } from "./Main/navigationSideBar";
import { ServerConfigBase } from "codeshell/core";
import { ServerConfig } from "./ServerConfig";

@NgModule({
    declarations: [Login, topBar, navigationSideBar],

    imports: [
        CodeShellModule.forRoot(),
        BrowserModule,
        BrowserAnimationsModule,
        ToastrModule.forRoot()
    ],
    exports: [
        CodeShellModule,
        BrowserModule,
        BrowserAnimationsModule,
        ToastrModule,
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
                { provide: ServerConfigBase, useValue: ServerConfig.Instance },
            ]
        }
    }
}