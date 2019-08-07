﻿import * as Moment from "moment";
import { RecursionModel } from "../Helpers/RecursionModel";
import { DeleteResult, NoteType } from "../Helpers";
import { Shell } from "../Shell";
import { TreeNode } from "angular-tree-component";

export function List_RemoveItem(lst: any[], item: any) {
    let ind = lst.indexOf(item);
    if (ind > -1)
        lst.splice(ind, 1);
}

export function List_RunRecursively_Nodes(lst: TreeNode[], func: (m: TreeNode) => void) {
    for (let mod of lst) {
        func(mod);
        if (mod.children && mod.children.length > 0)
            List_RunRecursively_Nodes(mod.children, func);
    }
}

export function List_FindIdRecusive(lst: RecursionModel[], id: number): RecursionModel | null {
    for (let mod of lst) {
        if (mod.id == id) {
            return mod;
        } else if (mod.children && mod.children.length > 0) {
            var m = List_FindIdRecusive(mod.children, id);
            if (m)
                return m;
        }
    }
    return null;
}

export function List_RunRecursively(lst: RecursionModel[], func: (m: RecursionModel) => void) {
    for (let mod of lst) {
        func(mod);
        if (mod.children && mod.children.length > 0)
            List_RunRecursively(mod.children, func);
    }
}

export function List_RunRecursively_GEN<T>(lst: T[], func: (m: T) => void) {

    for (let mod of lst) {
        console.log(typeof (mod as any).children)
        func(mod);
        if ((mod as any).children && (mod as any).children.length > 0)
            List_RunRecursively_GEN<T>((mod as any).children, func);
    }
}

export function List_RunRecursivelyUp_GEN<T>(mod: T, func: (m: T) => void, first: boolean = true) {
    if (!first)
        func(mod)
    let Par = (mod as any).parent;

    if (Par)
        List_RunRecursivelyUp_GEN<T>(Par, func, false);

}

export function List_ToggleItem(lst: any[], item: any) {
    let ind = lst.indexOf(item);
    if (ind > -1)
        lst.splice(ind, 1);
    else
        lst.push(item);
}

export function String_GetBeforeLast(data: string, del: string): string {
    var x = data.lastIndexOf(del);
    return data.substr(0, x);
}

export function String_GetAfterLast(data: string, del: string): string {
    var x = data.lastIndexOf(del);
    if (x == -1)
        return data;
    return data.substr(x + 1);
}

export function Date_Get(ev: Date | string): Date {
    let inp: Date = ev as Date;

    if (typeof ev == "string")
        inp = new Date(Date.parse(ev as string));


    return inp;
}

export function Date_Elapsed(start: Date, end: Date): Moment.Duration | null {
    if (end < start)
        return null;

    let endMoment = Moment(end);
    let startMoment = Moment(start);

    return Moment.duration(endMoment.diff(startMoment));
}

export class Utils {
    private static i: number = 0;
    private static lastSec: number;

    public static GetIdString(): string {
        let sec = new Date().getTime();
        let gen: string = "";
        if (sec == this.lastSec) {
            this.i++;
        } else {
            this.i = 0;
        }
        gen = (sec.toString() + (this.i));

        this.lastSec = sec;
        return gen;
    }

    public static GetId(): number {
        var s = [];

        return Number.parseInt(this.GetIdString());
    }

    public static Combine(...items: string[]): string {
        let endsWithSlash = new RegExp("\/$");
        let startsWithSlash = new RegExp("^\/");
        let final = "";
        for (var i = 0; i < items.length; i++) {
            var st = items[i];

            if (startsWithSlash.test(st))
                st = st.substr(1, st.length);

            if (i == (items.length - 1)) {
                if (endsWithSlash.test(st))
                    st = st.substr(0, st.length - 1);
            } else {
                if (!endsWithSlash.test(st))
                    st = st + "/";
            }
            final += st;
        }

        return final;
    }

    public static ConvertEnumToList(E: any): any[] {
        const keys = Object.keys(E).filter(k => typeof E[k as any] === "number"); // ["A", "B"]
        const values = keys.map(k => E[k as any]); // [0, 1]
        var list: any[] = [];
        for (var i = 0; i < keys.length; i++) {
            list.push({ key: keys[i], value: values[i] })
        }

        return list;
    }
}