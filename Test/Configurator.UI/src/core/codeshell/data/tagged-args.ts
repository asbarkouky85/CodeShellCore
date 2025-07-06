import { ListItem } from './list-item';

export class TaggedArgs {
    Data: any[] = [];
    Source: ListItem[] = [];
    /**  fromData: From Lookup, fromSrc: From selected items => Should return expression if arg1 exists in items */
    Comparer?: (fromData: any, fromSrc: any) => boolean = (d, s) => true;
    CreateNew?: (fromData: any) => any = d => { };
}