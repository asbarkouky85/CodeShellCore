import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpModule } from "@angular/http";
import { RouterModule } from "@angular/router";

import { MatDatepickerModule, MatNativeDateModule } from "@angular/material";
import { NgSelectModule } from "@ng-select/ng-select";
import { AngularDateTimePickerModule } from "angular2-datetimepicker";
import { MatMomentDateModule } from "@angular/material-moment-adapter";
import { TranslateModule, TranslateLoader } from "@ngx-translate/core";
import { AgmCoreModule, LazyMapsAPILoaderConfigLiteral } from "@agm/core";
import { DialogModule } from "primeng/dialog";
import { TreeModule } from "angular-tree-component";
import { CalendarModule } from 'primeng/calendar';
import {
    BsFormGroup, ShowIf, OnEnter, FixDate, SlimScroll, TestLoader, ImagePreLoad,
    FixDateTime, FixSelect, ListItemWatcher, Editable, Radio, DirctionFix
} from "CodeShell/Directives";
import { MaxValidator, IsUnique, DateValidator } from "CodeShell/Validators";
import { Paginate, SearchGroup } from "CodeShell/Components";
import { Translator } from "CodeShell/Localization/Translator";
import { ToggleButtonModule } from 'primeng/togglebutton';
import { ConfirmDialogComponent } from "./Components/confirm-dialog.component";
export function GetGoogleConfig(): LazyMapsAPILoaderConfigLiteral {
    return {
        apiKey: "AIzaSyD2ZSFjCvupPXGlyAKNhK7xcFtAuZQCBCQ"
    };
}

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpModule,
        DialogModule,
        RouterModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatMomentDateModule,
        AngularDateTimePickerModule,
        NgSelectModule,
        TreeModule.forRoot(),
        AgmCoreModule.forRoot(GetGoogleConfig()),
        TranslateModule.forRoot({ loader: { provide: TranslateLoader, useValue: Translator.GetTranslator() } }), ToggleButtonModule
    ],
    declarations: [
        BsFormGroup, ShowIf, OnEnter, FixDate, SlimScroll, TestLoader, ImagePreLoad,
        FixDateTime, FixSelect, ListItemWatcher, Editable, Radio,
        MaxValidator, IsUnique, DateValidator,
        Paginate, SearchGroup, DirctionFix, ConfirmDialogComponent
    ],
    exports: [
        CommonModule,
        FormsModule,
        HttpModule,
        DialogModule, CalendarModule,
        RouterModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatMomentDateModule, AngularDateTimePickerModule,
        NgSelectModule,
        TreeModule,
        AgmCoreModule,
        TranslateModule,
        ToggleButtonModule,

        BsFormGroup, ShowIf, OnEnter, FixDate, SlimScroll, TestLoader, ImagePreLoad,
        FixDateTime, FixSelect, ListItemWatcher, Editable, Radio,
        MaxValidator, IsUnique, DateValidator,
        Paginate, SearchGroup, DirctionFix
    ],
    entryComponents: [ConfirmDialogComponent]
})
export class CodeShellModule { }