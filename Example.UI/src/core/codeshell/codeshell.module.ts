import { NgModule, ModuleWithProviders } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatMomentDateModule } from "@angular/material-moment-adapter";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";
import { TranslateModule, TranslateLoader } from "@ngx-translate/core";
import { QuillModule } from "ngx-quill";

import { NgSelectModule } from "@ng-select/ng-select";
import { TreeModule } from '@circlon/angular-tree-component';

import {
    BsFormGroup, ShowIf, OnEnter, SlimScroll, ComponentLoader, ImagePreLoad,
    ListItemWatcher, Editable, Radio, DirctionFix, FixDateTime, FixDate,
    Selectable, FileUploader
} from "./directives";
import { IsUnique, ModalValidator, DateValidator, NumberRangeValidator } from "./validators";
import { Paginate, SearchGroup, DurationInput } from "./components";
import { TranslationService, NgxTranslationService, Translator } from "./localization";
import { AuthFilter } from "./security";
import { AuthorizationServiceBase } from "./security/authorizationServiceBase";
import { DialogModule } from 'primeng/dialog';
import { TokenStorage } from "./security/tokenStorage";
import { AbsoluteUrl } from "./pipes/absoluteUrl";

import { MatNativeDateModule } from "@angular/material/core";
import { OwlDateTimeModule, OwlNativeDateTimeModule } from "@danielmoncada/angular-datetime-picker";

@NgModule({

    imports: [
        
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        RouterModule,
        NgSelectModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatMomentDateModule,
        OwlDateTimeModule,
        OwlNativeDateTimeModule,
        DialogModule,
        QuillModule.forRoot(),
        TranslateModule.forRoot({ loader: { provide: TranslateLoader, useClass: Translator } }),
        TreeModule
    ],
    declarations: [
        BsFormGroup, ShowIf, OnEnter, SlimScroll,
        ComponentLoader, ImagePreLoad,
        ListItemWatcher, Editable, Radio,
        NumberRangeValidator, IsUnique, ModalValidator,
        Paginate, SearchGroup, DirctionFix,
        FixDate, DateValidator,
        Selectable, DurationInput, FileUploader,
        AbsoluteUrl
    ],
    exports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        RouterModule,
        NgSelectModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatMomentDateModule,
        OwlDateTimeModule,
        OwlNativeDateTimeModule,
        QuillModule,
        RouterModule,
        TranslateModule,
        TreeModule,
        DialogModule,

        FixDate, DateValidator,
        BsFormGroup, ShowIf, OnEnter, SlimScroll,
        ComponentLoader, ImagePreLoad,
        ListItemWatcher, Editable, Radio,
        NumberRangeValidator, IsUnique, ModalValidator,
        Paginate, SearchGroup, DirctionFix,
        Selectable, DurationInput, FileUploader,
        AbsoluteUrl
    ],
})
export class CodeShellModule {
    static forRoot(): ModuleWithProviders<CodeShellModule> {
        return {
            ngModule: CodeShellModule,
            providers: [
                AuthFilter,
                { provide: TranslationService, useClass: NgxTranslationService },
                AuthorizationServiceBase,
                TokenStorage
            ]
        }
    }
}