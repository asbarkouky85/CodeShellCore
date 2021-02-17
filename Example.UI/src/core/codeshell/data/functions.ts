
export function List_RemoveItem(lst: any[], item: any) {
    let ind = lst.indexOf(item);
    if (ind > -1)
        lst.splice(ind, 1);
}



export function List_OrderBy<T>(arr: Array<T>, del: (m: T) => any): Array<T> {
    arr.sort((a, b) => {
        var v1 = del(a);
        var v2 = del(b);
        if (v1 > v2)
            return 1;
        else if (v1 < v2)
            return -1;
        else
            return 0;
    });
    return arr;
}

export function List_OrderByDesc<T>(arr: Array<T>, del: (m: T) => any): Array<T> {
    arr.sort((a, b) => {
        var v1 = del(a);
        var v2 = del(b);
        if (v1 > v2)
            return -1;
        else if (v1 < v2)
            return 1;
        else
            return 0;
    });
    return arr;
}

export function List_ToggleItem(lst: any[], item: any) {
    let ind = lst.indexOf(item);
    if (ind > -1)
        lst.splice(ind, 1);
    else
        lst.push(item);
}