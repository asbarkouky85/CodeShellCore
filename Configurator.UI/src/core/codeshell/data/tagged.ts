import { TaggedArgs } from './tagged-args';
import { ListItem } from "./list-item";

export class Tagged {
    static FromDB(arg0: any): Tagged {
        var s: Tagged = Object.assign(new Tagged, arg0);
        s.Tag = new ListItem;
        return s;
    }
    public Tag: ListItem = new ListItem();

    public static CastList(lst: any[]): Tagged[] {
        for (var i in lst) {
            lst[i] = Tagged.FromDB(lst[i]);
        }
        return lst;
    }

    public static JoinLists_GEN<T extends Tagged>(con:new () => T, args: TaggedArgs): T[] {
        let lst: T[] = [];

        let comparer: (fromData: any, fromSrc: any) => boolean = (d, s) => false;
        let createNew: (i: any) => any = (item: any) => { };
        if (args.Comparer)
            comparer = args.Comparer;
        if (args.CreateNew)
            createNew = args.CreateNew;

        for (let dev of args.Data) {
            let d: T = Object.assign(new con, dev);

            let t = args.Source.find(e => comparer(dev, e));
            if (t) {
                t.selected = true;
            } else {
                t = ListItem.Detached(createNew(dev));
            }
            d.Tag = t;
            lst.push(d);
        }


        return lst;
    }

    public static JoinLists(args: TaggedArgs): Tagged[] {
        let lst: Tagged[] = [];

        let comparer: (fromData: any, fromSrc: any) => boolean = (d, s) => false;
        let createNew: (i: any) => any = (item: any) => {};
        if (args.Comparer)
            comparer = args.Comparer;
        if (args.CreateNew)
            createNew = args.CreateNew;

        for (let dev of args.Data) {
            let d: Tagged = Object.assign(new Tagged, dev);

            let t = args.Source.find(e => comparer(dev, e));
            if (t) {
                t.selected = true;
            } else {
                t = ListItem.Detached(createNew(dev));
            }
            d.Tag = t;
            lst.push(d);
        }


        return lst;
    }
}