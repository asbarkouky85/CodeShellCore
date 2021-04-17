import { Directive, ElementRef, ViewChild, ContentChild } from "@angular/core";

@Directive({ selector: "[bs-group]", exportAs: "bsFormGroup" })
export class BsFormGroup {

    TextInput: HTMLInputElement | null = null;
    SelectInput: HTMLSelectElement | null = null;
    TextareaInput: HTMLTextAreaElement | null = null;
    RadioInput: HTMLElement | null = null;
    OtherInput: HTMLElement | null = null;

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
        let rad: Element | null = this.Group.querySelector(".radio-group");

        if (rad) {
            this.RadioInput = rad as HTMLElement;
        } else {
            this.TextInput = this.Group.querySelector("input")
            this.SelectInput = this.Group.querySelector("select");
            this.TextareaInput = this.Group.querySelector("textarea");
            this.OtherInput = this.Group.querySelector(".input-control") as HTMLElement | null;
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
                var s = inps.item(i)
                if (s)
                    s.disabled = !this._enabled;
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
            var ite = this.SelectInput.selectedOptions.item(0)
            if (ite) {
                return ite.innerHTML;
            }

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