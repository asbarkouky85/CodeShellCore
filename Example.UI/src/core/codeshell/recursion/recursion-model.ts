export class RecursionModel {
    id: number=0;
    name: string="";
    children: any[] = [];
    editing: boolean = false;
    parentId: number | null = null;
    chain: string = "";
    nameChain: string = "";
    hasContents?: boolean;
    isExpanded: boolean = false;

    contentCount?: number;


    static FromDB(item: any, lst?: number[]): RecursionModel {
        let ret: RecursionModel = Object.assign(new RecursionModel, item);
        if (ret.children.length > 0) {
            for (var i in ret.children)
                ret.children[i] = this.FromDB(ret.children[i]);

        }
        return ret;
    }
}