import { Directive, ElementRef, HostListener } from "@angular/core";

@Directive({ selector: "[rtl-fix]", exportAs: "rtl-fix" })
export class DirctionFix {

    get Element(): HTMLElement { return this.el.nativeElement as HTMLElement; }
    constructor(private el: ElementRef) {

    }

    @HostListener("textContent") Change() {
        
    }

    ngOnInit() {
        setTimeout(() => {
            this.setDirection()
        }, 500)
        
    }

    setDirection() {
        var arabic = /[\u0600-\u06FF]/;
        
        this.Element.style.display = "inline-block";
        if (arabic.test(this.Element.innerText)) {
            this.Element.style.direction = "rtl";
        } else {
            this.Element.style.direction = "ltr";
        }
    }
}