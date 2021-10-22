import { from } from 'rxjs';
import { IModel } from "./models";

export class ListItem implements IModel {
    id: number = 0;
    state: string = "Detached";

    selected: boolean = false;

    static IsEmpty(items: ListItem[]): boolean {
        if (!items || items.length == 0)
            return true;
        return !items.some(d => d.state != "Removed");
    }

    static GetLength(items: ListItem[]): number {
        if (!items)
            return 0;
        return items.filter(e => e.state != 'Removed' && e.state != 'Detached').length;
    }

    public static GetChangedItems<T extends ListItem>(items: T[]): T[] {

        return items.filter(e => e.state == "Added" || e.state == "Modified" || e.state == "Removed");
    }

    public static GetAdded(items: ListItem[]): ListItem[] {

        return items.filter(e => e.state == "Added");
    }

    public static GetModifiedOrDeleted(items: ListItem[]): ListItem[] {

        return items.filter(e => e.state == "Modified" || e.state == "Removed");
    }

    public static HasChanges(items: ListItem[]): boolean {

        return items.some(e => e.state == "Added" || e.state == "Modified" || e.state == "Removed");
    }

    public static FromDB(obj: any): ListItem {
        let it: ListItem = Object.assign(new ListItem, obj);
        it.selected = true;
        it.state = "Attached";
        return it;
    }

    public static FromDB_GEN<T extends ListItem>(con: new () => T, obj: any): T {
        let it: T = Object.assign(new con, obj);

        it.selected = true;
        it.state = "Attached";
        return it;
    }

    public static Detached(obj: any): ListItem {
        let it: ListItem = Object.assign(new ListItem, obj);
        it.selected = false;
        it.state = "Detached";
        return it;
    }

    public static Detached_GEN<T extends ListItem>(con: new () => T, obj: any): T {
        let it: T = Object.assign(new con, obj);
        it.selected = false;
        it.state = "Detached";
        return it;
    }

    AddToChangeList() {
        if (this.state == "Detached" || !this.state) {
            this.state = "Added";
        } else if (this.state == "Attached") {
            this.state = "Modified";
        }
    }

    public SetModified() {

        if (this.state != "Added" && this.state != "Detached")
            this.state = "Modified";
    }

    public SetRemoved() {

        if (this.state != "Added")
            this.state = "Removed";
    }

    public SetAdded() {
        if (this.state == "Removed")
            this.state = "Attached";
        else if (this.state != "Modified" && this.state != "Attached")
            this.state = "Added";
    }

    public SetAttached() {
        var r = ["Added", "Removed", "Modified"];
        if (r.indexOf(this.state) > -1)
            this.state = "Attached";
    }

    public ApplyTo(items: any[]) {


        if (this.selected)
            this.AddTo(items);
        else
            this.RemoveFrom(items);


    }

    public RemoveFrom(items: any[]) {

        if (this.state == "Added") {
            let ind = items.indexOf(this);
            if (ind > -1)
                items.splice(ind, 1);
            this.state = "Detached";
        } else {
            this.state = "Removed";
        }
        this.selected = false;
    }

    public AddTo(items: any[]) {
        if (this.state == "Removed") {
            this.state = "Attached";
        } else if (this.state != "Added" && this.state != "Attached") {
            this.state = "Added";
            items.push(this);
        }
        this.selected = true;
    }

    public SelectOnly(items: any[]) {
        let x: ListItem[] = [];
        for (let d of items)
            x.push(d);
        for (let d of x)
            d.RemoveFrom(items);
        this.AddTo(items);
    }

    public static Convert(lst: any[]): ListItem[] {
        let ret: ListItem[] = [];
        for (var i in lst)
            ret[i] = ListItem.FromDB(lst[i]);
        return ret;
    }

    public static Convert_GEN<T extends ListItem>(con: new () => T, lst: any[]): T[] {
        let ret: T[] = [];
        for (var i in lst)
            ret[i] = ListItem.FromDB_GEN<T>(con, lst[i]);
        return ret;
    }



}

export class BoundListItem<T> extends ListItem {
    Bound?: T;
}
