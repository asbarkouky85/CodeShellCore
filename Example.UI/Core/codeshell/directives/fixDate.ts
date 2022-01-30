import { Directive, HostListener } from "@angular/core";
import { NgModel } from "@angular/forms";
import { MatDatepickerInputEvent } from "@angular/material";
import { Moment } from "moment";
import { DatePicker } from "angular2-datetimepicker";
import {Date_Get} from "../utilities/utils";

@Directive({
    selector: "[fix-date]",
    exportAs: "fix-date"
})
export class FixDate {

    constructor() { }
    @HostListener("dateChange", ['$event']) Change(ev: MatDatepickerInputEvent<Moment>) { }
}

@Directive({
    selector: "angular2-date-picker[ngModel][fix-date-time]",
    exportAs: "fix-date-time"
})
export class FixDateTime {
    constructor(private el: DatePicker, private model: NgModel) {
        this.el.date = new Date;
        this.el.onDateSelect.subscribe((ev: Date) => {

            this.model.viewToModelUpdate(Date_Get(ev));
        })
    }

    ngOnInit() {
        this.el.settings.bigBanner = true;
        this.el.settings.timePicker = true;
        this.el.settings.format = 'dd-MM-yyyy hh:mm a';
        this.el.settings.defaultOpen = false;
    }
}