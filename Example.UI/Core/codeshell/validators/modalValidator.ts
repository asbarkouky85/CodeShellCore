import { Directive, Input } from "@angular/core";
import { NgForm, FormControl, NgModel, DefaultValueAccessor, NgControl } from "@angular/forms";

@Directive({ selector: "[modal-required]", exportAs: "modal-required" })
export class ModalValidator {
    _isValid: boolean = true;
    

    @Input("fieldName")
    fieldName: string = "";
    control: FormControl = new FormControl();

    @Input("modal-required")
    get IsValid(): boolean {
        return this._isValid;
    }

    set IsValid(val: boolean) {

        if (val) {
            this.control.setErrors({ required_modal: null });
            this.control.setValue("v");
            this.control.updateValueAndValidity();
        } else {
            this.control.setValue(null);
            this.control.setErrors({ required_modal: true });
            //this.control.updateValueAndValidity();
        }
        this._isValid = val;
    }

    constructor(private frm: NgForm) {}

    

    ngOnInit() {
        this.frm.control.addControl(this.fieldName, this.control);
        this.frm.controls[this.fieldName] = this.control;
        this.control.markAsTouched();
        this.IsValid = false;
    }

    ngOnDestroy(){
        this.frm.control.removeControl(this.fieldName);
    }
}