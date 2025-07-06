import { Directive, HostListener } from "@angular/core";

import { Moment } from "moment";
import { MatDatepickerInputEvent } from "@angular/material/datepicker";

@Directive({
    selector: "[fix-date]",
    exportAs: "fix-date"
})
export class FixDate {

    @HostListener("dateChange", ['$event']) Change(ev: MatDatepickerInputEvent<Moment>) { }
}

