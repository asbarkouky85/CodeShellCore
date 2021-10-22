
import { Observable } from "rxjs";
import { EventEmitter } from "@angular/core";
import { List_RemoveItem } from '../data/functions';

export class ListSelectionService {


    List: any[] = [];
    Ids: number[] = [];
    itemsSelected: boolean = false;


    private _last: number[] = [];
    private selectStart: number = -1;
    private _itemsSelectedChange = new EventEmitter<boolean>();
    private _selectionChange = new EventEmitter<void>();

    get ItemsSelectedChange(): Observable<boolean> {
        return this._itemsSelectedChange;
    }

    get SelectionChanged(): Observable<void> {
        return this._selectionChange;
    }

    get SelectedItems(): any[] {
        return this.List.filter(d => d.rowSelected == true);
    }

    constructor() { }

    private _updateSelectionState() {
        var anySelected = this.Ids.length > 0;
        if (anySelected != this.itemsSelected)
            this._itemsSelectedChange.emit(anySelected);
        this.itemsSelected = anySelected;
        if (JSON.stringify(this._last) != JSON.stringify(this.Ids))
            this._selectionChange.emit();
        this._last = this.Ids;
    }

    SetItemSelectionStatus(item: any, status: boolean, only: boolean = false) {
        if (only) {
            for (var i of this.List)
                i.rowSelected = false;
            status = true;
        }
        if (!status) {
            List_RemoveItem(this.Ids, item.id);
            item.rowSelected = false;

        } else {
            if (only) {
                this.Ids = [item.id];

            } else {
                this.Ids.push(item.id);
            }
            item.rowSelected = true;
        }
        this._updateSelectionState();
    }


    ItemClicked(item: any, event: MouseEvent) {

        if (!event.shiftKey) {
            this.selectStart = this.List.indexOf(item);
            this.SetItemSelectionStatus(item, !item.rowSelected, !event.ctrlKey);
        } else {

            var current = this.List.indexOf(item);
            var selStart = current > this.selectStart ? this.selectStart : current;
            var selEnd = current > this.selectStart ? current : this.selectStart;
            for (var i = selStart; i <= selEnd; i++) {
                var it = this.List[i];
                this.SetItemSelectionStatus(it, true);
            }
        }
    }

    ClearSelection() {
        for (var i of this.List)
            i.rowSelected = false;

        this.Ids = [];
        this._updateSelectionState();
    }
}