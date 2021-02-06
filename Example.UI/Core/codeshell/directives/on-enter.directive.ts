import { Directive, EventEmitter, Output, HostListener, ElementRef } from "@angular/core";

@Directive({ selector:"input[onEnter]" })
export class OnEnter{
    @Output("onEnter") out: EventEmitter<void> = new EventEmitter<void>();

    @HostListener("keypress", ['$event']) OnKeyPress(e: KeyboardEvent) {
        if (e.key == "Enter")
            this.out.emit();
    }

}
