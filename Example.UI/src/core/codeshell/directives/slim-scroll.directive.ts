import { Directive, ElementRef, Input } from "@angular/core";

@Directive({
    selector: "[slimScroll]",
    exportAs: "slimScroll"
})
export class SlimScroll {
    @Input("slimScroll") height: string = '250px';

    constructor(private el: ElementRef) {
        
    }

    ngOnInit() {
        // $(this.el.nativeElement).slimScroll({
        //     position: 'left',
        //     height: this.height,
        //     railColor: "white",
        //     railOpacity: 0.5
        // });
    }

    ngOnChanges() {
        
    }
}