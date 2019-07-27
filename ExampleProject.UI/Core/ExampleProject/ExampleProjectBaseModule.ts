﻿import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { ModuleWithProviders } from "@angular/compiler/src/core";

import { ToastrModule } from "ngx-toastr";
import { TranslateModule } from "@ngx-translate/core";

import { CodeShellModule } from "codeshell";
import { CodeShellLocalizationModule } from "codeshell/localization";
import { CodeShellDatesModule } from "codeshell/dates";
import { CodeShellRecursionModule } from "codeshell/recursion";

import { AuthFilter } from "codeshell/security";
import { ServerConfigBase } from "codeshell/core";
import { ServerConfig } from "./ServerConfig";

@NgModule({
    declarations: [],

    imports: [
        CodeShellModule,
        CodeShellLocalizationModule,
        CodeShellDatesModule,
        CodeShellRecursionModule,

        BrowserModule,
        BrowserAnimationsModule,
        ToastrModule.forRoot()
    ],
    exports: [
        CodeShellModule,
        CodeShellLocalizationModule,
        CodeShellDatesModule,
        CodeShellRecursionModule,
        BrowserModule,
        BrowserAnimationsModule,
        ToastrModule,
        TranslateModule
    ]
})
export class ExampleProjectModule {
    static forRoot(): ModuleWithProviders {
        return {
            ngModule: ExampleProjectModule,
            providers: [
                AuthFilter,
                { provide: ServerConfigBase, useValue: ServerConfig.Instance },
            ]
        }
    }
}