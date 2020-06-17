import { List_RemoveItem } from "codeshell/helpers";
import { Observable } from "rxjs";
import { EventEmitter } from "@angular/core";

export class ListSelectionService {

    
    List: any[] = [];
    Ids: number[] = [];
    itemsSelected: boolean = false;
    
    private selectStart: number = -1;
    private _itemsSelectedChange = new EventEmitter<boolean>();

    get ItemsSelectedChange(): Observable<boolean> {
        return this._itemsSelectedChange;
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