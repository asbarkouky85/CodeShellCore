import { Directive, Input, ElementRef, HostListener, Output } from "@angular/core";
import { NgForOfContext, NgForOf } from "@angular/common";

@Directive({ selector: '[show-if]' })
export class ShowIf {
    @Input("show-if") Condition: boolean = true;

    get Style(): CSSStyleDeclaration { return this.el.nativeElement.style; }

    constructor(private el: ElementRef) { }

    ngOnInit() {
        if (!this.Condition) {
            this.Style.display = "none";
        }
    }

    ngOnChanges() {
        if (!this.Condition) {
            this.Style.display = "none";
        } else {
            this.Style.removeProperty("display");
        }
    }
}

@Directive({ selector: '[ngFor][ngForEditable]', exportAs:"editable" })
export class Editable {
    @Input("ngForEditable")
    Ed?: any;
    constructor(private ngFor: NgForOf<any>) {

    }

    ngOnInit() {
        
    }
}