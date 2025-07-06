import { EventEmitter } from "@angular/core";
import { Observable } from "rxjs";
import { List_RemoveItem } from "codeshell/data";

export class SectionedFormService {
    CurrentIndex: number = 1;
    CompleteIndex: number = 0;
    ActiveTabs: number[] = [];
    Steps: boolean = false;
    Element?: HTMLElement;
    TabCount: number = 0;
    private _change = new EventEmitter<number[]>();
    private _allComplete = new EventEmitter<void>();
    private _validations: { [id: number]: boolean } = {};
    private _validStatus = new EventEmitter<boolean>();
    private _isValid = true;

    get OnChange(): Observable<number[]> { return this._change; }
    get OnAllComplete(): Observable<void> { return this._allComplete; }
    get PecentComplete() { return ((this.CompleteIndex) / (this.TabCount - 1)) * 100; }
    get OnValidChanged(): Observable<boolean> { return this._validStatus; }
    get IsValid() { return this._isValid; }

    constructor(tabs: number, active: number[] = [], defaultState = true, steps = false) {
        this.TabCount = tabs;
        this.Steps = steps;
        if (defaultState) {

            for (let i = 1; i <= tabs; i++)
                this.ActiveTabs.push(i);

        } else {
            this.ActiveTabs = active;
        }
        setTimeout(() => {
            this._change.emit(this.ActiveTabs);
        }, 200)
    }

    Select(index: number, scroll?: string) {
        this.ActiveTabs = [index];
        this.CurrentIndex = index;
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

    SetValidState(index: number, state: boolean) {

        this._validations[index] = state;
        var current = true;
        for (var v in this._validations) {
            if (this._validations[v] == false) {
                current = false;
                break;
            }
        }
        
        if (this._isValid != current) {
            this._validStatus.emit(current);
            this._isValid = current;
        }
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

    AllComplete() {
        this._allComplete.emit();
    }

    Next() {
        this.CompleteIndex = this.CurrentIndex;
        this.CurrentIndex += 1;
        this.ActiveTabs = [this.CurrentIndex];
    }

    Back() {
        if (this.CurrentIndex == 1)
            return;
        this.CompleteIndex -= 1;
        this.CurrentIndex -= 1;
        this.ActiveTabs = [this.CurrentIndex];
    }


    IsLast() {
        return this.CurrentIndex == this.TabCount;
    }

    CanGoBack() {
        return this.CurrentIndex > 1;
    }

    CanGoNext() {
        return this.CurrentIndex < this.TabCount;
    }

    IsComplete(index: number) {

        return index <= this.CompleteIndex;
    }

    IsActive(index: number) {
        return this.ActiveTabs.indexOf(index) > -1;
    }

    IsCurrent(index: number) {
        return this.CurrentIndex == index;
    }
}