import { BaseComponent } from "codeshell/baseComponents";
import { Injectable, ViewChild, ElementRef } from "@angular/core";

@Injectable()
export abstract class GenerationInofBase extends BaseComponent {

    @ViewChild("container") _containerRef?: ElementRef;

    Container?: HTMLElement;

    list: any[] = [];
    renderModel: any = {};

    ngOnInit() {
        if (this._containerRef)
            this.Container = this._containerRef.nativeElement;
    }

    Clear() {
        this.list = [];
    }

    AddMessage(d: any) {
        this.list.push(d);
        if (d.isNew)
            this.list.push({ newLine: true });
        if (this.Container) {
            this.Container.scroll(0, this.Container.scrollHeight);
        }

    }

}