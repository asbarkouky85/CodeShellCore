import { NgModule, ModuleWithProviders } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatMomentDateModule } from "@angular/material-moment-adapter";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";
import { TranslateModule, TranslateLoader } from "@ngx-translate/core";
import { QuillModule } from "ngx-quill";

import { AngularDateTimePickerModule } from "angular2-datetimepicker";
import { CalendarModule } from "primeng/calendar";
import { DialogModule } from "primeng/dialog";
import { NgSelectModule } from "@ng-select/ng-select";
import { TreeModule } from "angular-tree-component";

import {
    BsFormGroup, ShowIf, OnEnter, SlimScroll, TestLoader, ImagePreLoad,
    ListItemWatcher, Editable, Radio, DirctionFix, FixDateTime, FixDate,
    Selectable, FileUploader
} from "./directives";
import { IsUnique, ModalValidator, DateValidator, NumberRangeValidator } from "./validators";
import { Paginate, SearchGroup, DurationInput } from "./components";
import { TranslationService, NgxTranslationService, Translator } from "./localization";
import { AuthFilter } from "./security";
import { AuthorizationServiceBase } from "./security/authorizationServiceBase";

import { TokenStorage } from "./security/tokenStorage";
import { AbsoluteUrl } from "./pipes/absoluteUrl";

import { MatNativeDateModule } from "@angular/material/core";

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
        QuillModule,
        AngularDateTimePickerModule,
        DialogModule,
        TreeModule.forRoot(),
        TranslateModule.forRoot({ loader: { provide: TranslateLoader, useClass: Translator } }),
    ],
    declarations: [
        BsFormGroup, ShowIf, OnEnter, SlimScroll, TestLoader, ImagePreLoad,
        ListItemWatcher, Editable, Radio,
        NumberRangeValidator, IsUnique, ModalValidator,
        Paginate, SearchGroup, DirctionFix,
        FixDate, FixDateTime, DateValidator,
        Selectable, DurationInput, FileUploader,
        AbsoluteUrl
    ],
    exports: [
        CommonModule,
        FormsModule,
        HttpClientModule,
        RouterModule,
        NgSelectModule,
        CalendarModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatMomentDateModule,
        AngularDateTimePickerModule,
        DialogModule,
        TreeModule,
        TranslateModule,
        QuillModule,
        FixDate, FixDateTime, DateValidator,
        BsFormGroup, ShowIf, OnEnter, SlimScroll, TestLoader, ImagePreLoad,
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