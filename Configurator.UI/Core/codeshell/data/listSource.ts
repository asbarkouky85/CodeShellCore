import { LoadOptions, LoadResult, TaggedArgs, Tagged, PropertyFilter } from "../helpers";
import { ListSelectionService } from "codeshell/helpers/ListSelectionService";

export class ListSource {
    Loader: (opts: LoadOptions) => Promise<LoadResult>;
    TagArguments?: TaggedArgs;

    private _list: any[] = [];
    private _totalCount: number = 0;
    private _useSelect: boolean = false;
    // private _currentPage: number = 0;

    InitialSelection: number[] | null = null;
    Opts: LoadOptions = { Showing: 0, Skip: 0 }
    pageIndex: number = 0;
    get List(): any[] { return this._list; };
    get TotalCount(): number { return this._totalCount; };

    LoadedOnce: boolean = false;
    OnDataLoaded?: (lst: any[]) => void;
    UseJoin: boolean = true;
    get UseSelection(): boolean { return this.Selection != null; }
    set UseSelection(val: boolean) {
        this.Selection = new ListSelectionService();
    }
    Selection: ListSelectionService | null = null;


    constructor(showing: number, predicate: (opts: LoadOptions) => Promise<LoadResult>) {
        this.Loader = predicate;
        this.Opts.Showing = showing;
    }

    static get Empty(): ListSource {
        return new ListSource(10, e => Promise.resolve(new LoadResult));
    }

    Retag() {
        if (this.TagArguments)
            this._list = Tagged.JoinLists(this.TagArguments);
    }

    SelectById(id: number) {
        if (this.TagArguments) {
            let s: Tagged = this.List.find(d => d.id == id);

            if (s) {
                s.Tag.selected = true;
                s.Tag.AddTo(this.TagArguments.Source);
            }
        }
    }

    AfterLoad(e: LoadResult) {

        if (this.OnDataLoaded)
            this.OnDataLoaded(e.list);
        if (this.UseJoin) {
            if (this.TagArguments) {
                this.TagArguments.Data = e.list;
                this._list = Tagged.JoinLists(this.TagArguments);

            } else {
                this._list = [];
                for (var i in e.list)
                    this._list[i] = Object.assign(new Tagged, e.list[i]);
            }
        } else {
            this._list = e.list;
        }
        
        this._totalCount = e.totalCount;
        this.LoadedOnce = true;
        if (this.InitialSelection) {
            for (var id of this.InitialSelection)
                this.SelectById(id)
        }
    }

    async LoadDataAsync(): Promise<LoadResult>{
        var data = await this.Loader(this.Opts);
        this.AfterLoad(data);

        return data;
    }

    LoadData() {
       
        this.Loader(this.Opts).then(e => {
            this.AfterLoad(e);
        });
    }

    PageChanged(id: number) {
        this.Opts.Skip = this.Opts.Showing * id;
        this.LoadData();
    }

    Search(term: string) {
        this.Opts.SearchTerm = term;
        this.Opts.Skip = 0;
        this.pageIndex = 0;
        this.LoadData();
    }

    SetFilters(filters: PropertyFilter[]) {
        this.Opts.Filters = JSON.stringify(filters);
    }
}