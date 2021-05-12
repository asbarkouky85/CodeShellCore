export class PropertyFilter {
    MemberName: string = "";
    FilterType: "date"|"day"|"reference"|"equals"|"string"|"decimal"|"int"|"custom" = "reference";
    Value1?: string;
    Value2?: string;
    Ids: any[] = [];
}

export class StoredLoadOptions {
    route: string = "";
    options: LoadOptions = new LoadOptions();
}

export class LoadOptions {
    Showing: number = 0;
    Skip: number = 0;
    OrderProperty?: string;
    Direction?: string;
    SearchTerm?: string;
    Filters?: string;
    AsLookup?: boolean;

    static AddFilter(opts: LoadOptions, fil: PropertyFilter) {
        let arr: PropertyFilter[];

        if (!opts.Filters) {
            opts.Filters = "";
            arr = [];
        }
        else {
            let f = JSON.parse(opts.Filters);
            arr = Object.assign(new Array<PropertyFilter>(), f);
        }
        arr.push(fil);
        opts.Filters = JSON.stringify(arr);
    }
}