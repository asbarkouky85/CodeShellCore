import { Directive } from "@angular/core";
import { ListSelectionService } from "codeshell/helpers/ListSelectionService";
import { Input } from "@angular/core";
import { HostListener } from "@angular/core";
import { ElementRef } from "@angular/core";
import { HostBinding } from "@angular/core";

@Directive({ selector: "[selectable]", exportAs:"selectable" })
export class Selectable {

    @Input("selectable")
    model: any = {};

    @Input("select-service")
    Service?: ListSelectionService;

    Element: HTMLElement;

    constructor(ref: ElementRef) {
        this.Element = ref.nativeElement;
    }

    @HostBinding("class")
    get ngClass() { return this.model.rowSelected ? 'list-item selected' : 'list-item'; }

    @HostListener("click", ["$event"])
    OnClick(event: MouseEvent) {
        if (this.Service)
            this.Service.ItemClicked(this.model, event);
    }

}