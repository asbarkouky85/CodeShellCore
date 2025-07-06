import { Directive, Input, ElementRef } from "@angular/core";
import { NgModel } from "@angular/forms";
import { Date_Get } from "../utilities/utils";
import * as Moment from "moment";

@Directive({
    selector: '[date-validate][ngModel]',
    exportAs: 'date-validate'
})
export class DateValidator {
    @Input("min-date") Min?: any;
    @Input("max-date") Max?: any;
    IsRequired: boolean = false;


    @Input("date-validate") Type?: string;

    constructor(private model: NgModel, el: ElementRef) {
        let elem = el.nativeElement as HTMLElement;
        this.IsRequired = elem.hasAttribute("required");
    }

    compareDates(date1: Date, date2: Date): number {
        
        if (Moment.isMoment(date1)) {
            var e = date1 as Moment.Moment;
            date1 = e.toDate()
        }

        if (Moment.isMoment(date2)) {
            var e = date2 as Moment.Moment;
            date2 = e.toDate()
        }
        //debugger

        date1.setHours(0, 0, 0, 0);
        date2.setHours(0, 0, 0, 0);

        console.log(date1, date2);

        if (date1 >= date2)
            return 1;
        else
            return 2;
    }

    ngOnInit()
    {
        this.model.update.subscribe((v: Date) => {
            if (!Moment.isMoment(v))
            v = new Date(v);
            setTimeout(() => {
                let min = this.minDate();

                let max = this.maxDate();

                let isValid = true;
                if (min != null)
                {
                    isValid = this.compareDates(v, min) == 1;
                }

                if (max != null && isValid)
                {
                    isValid = this.compareDates(v, max) == 2;
                }

                if (isValid) {
                    this.model.control.setErrors({ date_validation: null });
                    this.model.control.updateValueAndValidity();
                }
                else {

                    this.model.control.setErrors({ date_validation: true });
                    
                }
            }, 200);
            

        })

    }

    minDate(): Date | null {
        if (this.Type == "future")
            return new Date();
        if (this.Min) {
            return Date_Get(this.Min);
        }
        return null;
    };

    maxDate(): Date | null {
        if (this.Type == "past")
            return new Date();
        if (this.Max) {
            return Date_Get(this.Max);
        }
        return null;
    };


}