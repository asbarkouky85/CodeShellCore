import { HostListener, ElementRef, SimpleChange, SimpleChanges, Injectable } from "@angular/core";
import { NgModel } from "@angular/forms";

@Injectable()
export abstract class ValidatorBase {

    @HostListener("blur", ["$event"])
    valueChanged(ev: Event) {
        this.runValidation();
    }

    ngOnChanges(ch: SimpleChanges) {
        if (this.RunIf(ch)) {
            this.runValidation();
        }
    }

    protected RunIf(ch: SimpleChanges): boolean {
        return false;
    }

    protected runValidation() {
        var err: any = {};
        if (this.IsValid()) {
            err[this.Identifier] = null;
            this.Model.control.updateValueAndValidity();
        } else {
            err[this.Identifier] = true;
            this.Model.control.setErrors(err);
        }
    }

    Element: HTMLElement;
    Model: NgModel;
    abstract Identifier: string;

    constructor(private el: ElementRef, model: NgModel) {
        this.Model = model;
        this.Element = el.nativeElement as HTMLElement;
    }

    abstract IsValid(): boolean;
}