import { NgModule, ModuleWithProviders } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatDatepickerModule, MatNativeDateModule } from "@angular/material";
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
    ListItemWatcher, Editable, Radio, DirctionFix, FixDateTime, FixDate
} from "./forms";
import { MaxValidator, IsUnique, ModalValidator, DateValidator } from "./forms";
import { Paginate, SearchGroup } from "./forms";
import { TranslationService, NgxTranslationService, Translator } from "./localization";
import { AuthFilter } from "./security";
import { AuthorizationServiceBase } from "./security/authorizationServiceBase";
import { Selectable } from "codeshell/directives/selectable";
import { DurationInput } from "./components/durationInput";
import { FileUploader } from "./directives/fileUploader";
import { TokenStorage } from "./security/tokenStorage";

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
        MaxValidator, IsUnique, ModalValidator,
        Paginate, SearchGroup, DirctionFix,
        FixDate, FixDateTime, DateValidator,
        Selectable, DurationInput, FileUploader
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
        MaxValidator, IsUnique, ModalValidator,
        Paginate, SearchGroup, DirctionFix,
        Selectable, DurationInput, FileUploader
    ],
})
export class CodeShellModule {
    static forRoot(): ModuleWithProviders {
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