import "codeshell/utilities/extensions";
import { List_RemoveItem } from "codeshell/helpers";
import { EventEmitter } from "@angular/core";
import { Observable } from "rxjs";

export class SectionedFormService {
    CurrentIndex: number = 0;
    CompleteIndex: number = 5;
    ActiveTabs: number[] = [];
    Steps: boolean = false;
    Element?: HTMLElement;
    private _change = new EventEmitter<number[]>();

    get OnChange(): Observable<number[]> { return this._change; }
    

    constructor(tabs: number, active: number[] = [], defaultState = true, steps = false) {
        this.Steps = steps;
        if (defaultState) {

            for (let i = 1; i <= tabs; i++)
                this.ActiveTabs.push(i);

        } else {
            this.ActiveTabs = active;
        }
        setTimeout(() => {
            this._change.emit(this.ActiveTabs);
        },200)
    }

    Select(index: number, scroll?: string) {
        this.ActiveTabs = [index];
        if (scroll)
            this.Scroll(scroll);
        this._change.emit(this.ActiveTabs);
    }

    Toggle(index: number, scroll?: string) {

        var loc = this.ActiveTabs.indexOf(index);

        if (loc == -1) {
            if (this.Steps) {
                if (index <= (this.CompleteIndex + 1)) {
                    this.CurrentIndex = index;
                    this.ActiveTabs.push(index);
                }
            } else {
                this.ActiveTabs.push(index);
            }

        } else {
            this.ActiveTabs.splice(loc, 1);
        }
        if (scroll)
            this.Scroll(scroll);
        this._change.emit(this.ActiveTabs);
    }

    SetComplete(index: number, force: boolean = false) {
        if (!force && this.CompleteIndex < index) {
            this.CompleteIndex = index;
        }
        List_RemoveItem(this.ActiveTabs, index);
        
        var nxt = index + 1;
        if (this.ActiveTabs.indexOf(nxt) == -1)
            this.ActiveTabs.push(nxt);
        this._change.emit(this.ActiveTabs);
    }

    Scroll(id: string) {
        let elem = document.getElementById(id);
        if (elem) {
            window.scrollTo({ top: elem.offsetTop });
        }
    }

    IsComplete(index: number) {

        return index <= this.CompleteIndex;
    }

    IsActive(index: number) {
        return this.ActiveTabs.indexOf(index) > -1;
    }
}