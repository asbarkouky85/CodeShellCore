import { Directive, Input, ElementRef, HostListener, SimpleChanges } from "@angular/core";
import { NgModel, NG_VALIDATORS, FormControl, Validator, AbstractControl, ValidationErrors } from "@angular/forms";
import { ValidatorBase } from "./validatorBase";

@Directive({ selector: '[numberRange][ngModel]', exportAs: 'numberRange' })
export class NumberRangeValidator extends ValidatorBase {
    Identifier = "number_range";
    private _max?: number;

    @Input("min")
    Min?: number;

    @Input("max")
    get Max(): number | undefined { return this._max; };
    set Max(val: number | undefined) { this._max = val; };

    constructor(el: ElementRef, mod: NgModel) {
        super(el, mod);
    }

    ngOnInit() {

        if (this.Max != undefined)
            (this.Element as HTMLInputElement).max = this.Max.toString();
    }

    protected RunIf(ch: SimpleChanges): boolean {
        return ch.Min != undefined || ch.Max != undefined;
    }

    IsValid(): boolean {
        var v = true;

        if (this.Max) {
            v = v && this.Model.value < this.Max;
        }

        if (this.Min) {
            v = v && this.Model.value > this.Min;
        }
        return v;
    }
}
