import { Directive, Input, ElementRef, HostListener } from "@angular/core";
import { ListItem } from "../helpers";

@Directive({ selector: "[li-watch]", exportAs: "li-watch" })
export class ListItemWatcher {
    item: ListItem | null = null;
    @Input("li-watch") mod?: any;

    @HostListener("change", ['$event']) Change(ev: any) {
        if (this.item)
            this.item.SetModified();
        
    }

    constructor(cont: ElementRef) {

    }

    ngOnInit() {

        if (this.mod instanceof ListItem) {
            this.item = this.mod;
        }
    }
}