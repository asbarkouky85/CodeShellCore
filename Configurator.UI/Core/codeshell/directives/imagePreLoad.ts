import { Directive, ElementRef } from "@angular/core";

@Directive({ selector: "[awaiter]", exportAs: "awaiter" })
export class ImagePreLoad {

    awaiterDiv?: HTMLElement;
    private isLoaded: boolean = false;

    constructor(private el: ElementRef) {
        let ht: HTMLImageElement = el.nativeElement;
        
        this.awaiterDiv = document.createElement("div");
        this.awaiterDiv.classList.add("awaiter");
        this.awaiterDiv.style.width = "100%";
        this.awaiterDiv.style.display = "inline-block";
        this.awaiterDiv.style.height = "256px";
        this.awaiterDiv.style.position = "relative";

        for (let i = 0; i < ht.classList.length; i++) {
            let x = ht.classList.item(i);
            if (x)
                this.awaiterDiv.classList.add(x);
        }

        $(ht).parent().append(this.awaiterDiv);
        $(ht).hide();
        ht.addEventListener("load", (e) => {

            if (ht.complete && ht.naturalWidth!==0) {
                $(ht).fadeIn(500);
                $(this.awaiterDiv).remove();
                this.isLoaded = true;
                return {};
            }
            
        })

        setTimeout(() => {
            if (!this.isLoaded) {
                $(ht).fadeIn(500);
                ht.style.height = this.getClosestWidth(ht) + "px";
                $(this.awaiterDiv).remove();
                this.isLoaded = true;
            }
        }, 2000);
    }

    getClosestWidth(el: HTMLElement): number {
        var w = el.getBoundingClientRect();

        if (w.width > 0)
            return w.width;
        else if (el.parentElement)
            return this.getClosestWidth(el.parentElement);
        else
            return 0;

    }

    ngOnInit() {
        if (this.awaiterDiv) {
            let ht: HTMLElement = this.el.nativeElement;
            var w = this.getClosestWidth(ht);
            this.awaiterDiv.style.height = (w == 0) ? "256px" : w + "px";
        }

    }
}