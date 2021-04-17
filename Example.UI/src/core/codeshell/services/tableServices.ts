
import { ListItem } from "codeshell/data/list-item";
import { List_RemoveItem } from "codeshell/data";

export class TableService {

    Adding: boolean = false;
    get List(): any[] { 
        return this.listRef(); 
    }

    constructor(private listRef: () => ListItem[]) {

    }

    private _removeAddRow() {
        var mod = this.List.find(d => d.addingRow == true);
        if (mod)
            List_RemoveItem(this.List, mod);
    }

    StartAdd() {

        this.Adding = true;
        this.List.push(ListItem.Detached({ addingRow: true }))

    }

    CancelAdd() {
        this._removeAddRow();
        this.Adding = false;
    }

    SubmitAdd() {
        var mod = this.List.find(d => d.addingRow == true);
        if (mod) {
            (mod as ListItem).SetAdded();
            mod.addingRow = false;
            this.StartAdd();
        }
    }
}