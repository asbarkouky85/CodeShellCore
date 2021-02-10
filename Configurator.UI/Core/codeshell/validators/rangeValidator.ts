import { Directive, Input, ElementRef } from "@angular/core";
import { NgModel, NG_VALIDATORS, FormControl, Validator, AbstractControl, ValidationErrors } from "@angular/forms";

@Directive({
    selector: '[max][ngModel]',
    exportAs: 'max',
    providers: [{
        provide: NG_VALIDATORS,
        useExisting: MaxValidator,
        multi: true
    }]
})
export class MaxValidator implements Validator {
    @Input("max") Max: number = 0;


    constructor(private el: ElementRef) {

    }

    ngOnInit() {

        (this.el.nativeElement as HTMLInputElement).max = this.Max.toString();
    }

    validate(c: AbstractControl): ValidationErrors | null {
        if (this.Max > 0) {
            if (c.value > this.Max)
                return { max: false }
        }

        return null;
    }
}