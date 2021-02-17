import { Directive } from "@angular/core";
import { NgModel } from "@angular/forms";
import { DatePicker } from "angular2-datetimepicker";
import { Date_Get } from 'codeshell/utilities/functions';



@Directive({
    selector: "angular2-date-picker[ngModel][fix-date-time]",
    exportAs: "fix-date-time"
})
export class FixDateTime {
    constructor(private model: NgModel, private el: DatePicker) {
        this.el.date = new Date;
        this.el.onDateSelect.subscribe((ev: Date) => {

            this.model.viewToModelUpdate(Date_Get(ev));
        });
    }

    ngOnInit() {
        this.el.settings.bigBanner = true;
        this.el.settings.timePicker = true;
        this.el.settings.format = 'dd-MM-yyyy hh:mm a';
        this.el.settings.defaultOpen = false;
    }
}
