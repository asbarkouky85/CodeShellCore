import { Directive, Input, HostListener, ElementRef, Output, EventEmitter } from "@angular/core";

@Directive({ selector: "input[type='radio'][radioCheck]", exportAs: 'radioCheck' })
export class Radio {
    @Input("radioCheck") model: boolean = false;
    @Output("radioCheckChange") modelOut: EventEmitter<boolean> = new EventEmitter<boolean>();

    get Element(): HTMLInputElement { return this.el.nativeElement as HTMLInputElement; }

    constructor(private el: ElementRef) {

    }

    ngOnInit() {
        this.Element.checked = this.model;
    }

    @HostListener("click")
    OnClick() {
        var elems = document.getElementsByName(this.Element.name);

        for (let i = 0; i < elems.length; i++) {
            var el = elems.item(i) as HTMLInputElement;
            if (el != this.Element && el.type == "radio") {
                el.dispatchEvent(new Event('change'));
            }
        }
    }

    @HostListener("change")
    OnChange() {
        this.modelOut.emit(this.Element.checked);
    }



}