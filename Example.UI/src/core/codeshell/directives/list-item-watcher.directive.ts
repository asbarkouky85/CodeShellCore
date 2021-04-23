import { Directive, Input, ElementRef, HostListener } from "@angular/core";
import { ListItem } from "../data";

@Directive({ selector: "[li-watch]", exportAs: "liWatch" })
export class ListItemWatcher {
    item: ListItem | null = null;
    @Input("li-watch") mod?: any;

    @HostListener("change", ['$event']) Change(ev: any) {
        if (this.item){
            this.item.SetModified();
        }
            
        
    }

    constructor(cont: ElementRef) {

    }

    ngOnInit() {

        if (this.mod instanceof ListItem) {
            this.item = this.mod;
        }
    }
}