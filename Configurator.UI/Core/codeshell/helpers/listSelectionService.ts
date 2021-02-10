import { List_RemoveItem } from "codeshell/helpers";

export class ListSelectionService {

    List: any[] = [];
    Ids: number[] = [];
    itemsSelected: boolean = false;
    selectStart: number = -1;

    constructor() { }

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
        this.itemsSelected = this.Ids.length > 0;
    }

    ClearSelection() {
        for (var i of this.List)
            i.rowSelected = false;
        this.itemsSelected = false;
        this.Ids = [];
    }
}