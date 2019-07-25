import { ModuleWithProviders } from "@angular/compiler/src/core";
import { NgModule } from "@angular/core";
import { CodeShellModule } from "CodeShell";
import { BrowserModule } from "@angular/platform-browser";
import { ToastrModule } from "ngx-toastr";
import { TranslateModule } from "@ngx-translate/core";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations"
import { Login } from "./Main/Login";
import { AuthFilter } from "CodeShell/security";
import { topBar } from "./Main/topBar";
import { navigationSideBar } from "./Main/navigationSideBar";

@NgModule({
    declarations: [Login, topBar, navigationSideBar],

    imports: [
        CodeShellModule,
        BrowserModule,
        BrowserAnimationsModule,
        ToastrModule.forRoot()
    ],
    exports: [
        CodeShellModule,
        BrowserModule,
        BrowserAnimationsModule,
        ToastrModule,
        TranslateModule,
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
                AuthFilter,
            ]
        }
    }
}