import { Directive, ElementRef, ViewChild, ContentChild } from "@angular/core";
import { DatePicker } from "angular2-datetimepicker";
import { HtmlAstPath } from "@angular/compiler";

@Directive({ selector: "[bs-group]", exportAs: "bsFormGroup" })
export class BsFormGroup {

    TextInput: HTMLInputElement | null = null;
    SelectInput: HTMLSelectElement | null = null;
    TextareaInput: HTMLTextAreaElement | null = null;
    RadioInput: HTMLElement | null = null;
    OtherInput: HTMLElement | null = null;

    @ContentChild(DatePicker) DateTimePicker?: DatePicker;

    private _parent: HTMLElement | null = null;
    private _enabled: boolean = true;

    get InputControl(): HTMLElement | null { return this._getInputControl(); }

    get Write(): boolean { return this._enabled; };
    get Group(): HTMLElement { return this.el.nativeElement as HTMLElement; }
    get Enabled(): boolean { return this._enabled; }
    set Enabled(val: boolean) {
        this._setEnabled(val);
    }

    get Value(): string { return this._getValue(); }

    constructor(private el: ElementRef) { }

    ngOnInit() {
        let rad: HTMLElement | null = this.Group.querySelector(".radio-group");
        if (this.DateTimePicker) {
            this.OtherInput = this.Group.querySelector("angular2-date-picker")
        } else if (rad) {
            this.RadioInput = rad;
        } else {
            this.TextInput = this.Group.querySelector("input")
            this.SelectInput = this.Group.querySelector("select");
            this.TextareaInput = this.Group.querySelector("textarea");
            this.OtherInput = this.Group.querySelector(".input-control");
        }


        if (this.InputControl)
            this._parent = this.InputControl.parentElement;
    }

    private _setEnabled(val: boolean) {
        if (this._enabled == val)
            return;
        this._enabled = val;

        if (this.RadioInput) {
            let inps = this.RadioInput.getElementsByTagName("input");
            for (let i = 0; i < inps.length; i++) {
                inps.item(i).disabled = !this._enabled;
            }

            return;
        }

        if (this._enabled == false) {
            
            if (this._parent)
                this._parent.removeChild(this.InputControl as HTMLElement);

        } else {
            if (this._parent)
                this._parent.appendChild(this.InputControl as HTMLElement);

        }
    }

    private _getValue(): string {
        if (this.TextInput)
            return this.TextInput.value;
        else if (this.SelectInput) {
            if (this.SelectInput.selectedOptions.length > 0)
                return this.SelectInput.selectedOptions.item(0).innerHTML;
        } else if (this.TextareaInput) {
            return this.TextareaInput.value;
        }
        return "";

    }

    private _getInputControl(): HTMLElement | null {
        if (this.TextInput)
            return this.TextInput;
        else if (this.SelectInput) {
            return this.SelectInput;
        } else if (this.TextareaInput) {
            return this.TextareaInput;
        } else if (this.OtherInput) {
            return this.OtherInput;
        }
        return null;
    }
}