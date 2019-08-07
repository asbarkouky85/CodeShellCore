import { Directive, Input, ElementRef } from "@angular/core";
import { NgModel } from "@angular/forms";
import { Date_Get } from "../Utilities/Utils";

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

    ngOnInit() {

        this.model.update.subscribe((v: Date) => {
            let min = this.minDate();
            let max = this.maxDate();
            let isValid = true;

            if (min != null) {
                isValid = isValid && v > min;
            }

            if (max != null) {
                isValid = isValid && v < max;
            }

            console.log(v, min);

            if (isValid) {
                this.model.control.setErrors({ date_validation: null });
                this.model.control.updateValueAndValidity();
            }
            else {
                this.model.control.setErrors({ date_validation: true });
            }

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