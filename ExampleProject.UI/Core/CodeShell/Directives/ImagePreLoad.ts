import { Directive, ElementRef } from "@angular/core";

@Directive({ selector: "[awaiter]", exportAs: "awaiter" })
export class ImagePreLoad {
    constructor(private el: ElementRef) {
        let ht: HTMLElement = el.nativeElement;
        console.log($(ht).height());
        let s = document.createElement("div");
        let lst: DOMTokenList = ht.classList;
        s.classList.add("awaiter");
        for (let i = 0; i < ht.classList.length; i++) {
            let x = ht.classList.item(i);
            if (x)
                s.classList.add(x);
        }
        
        $(ht).parent().append(s)
        $(ht).hide();
        ht.addEventListener("load", (e) => {
            $(ht).fadeIn(500);
            $(s).remove();
            return {};
        })
    }
}