import { BaseComponent } from "./base-component";
import { EntityHttpService } from "../http";
import { DeleteResult, LoadResult } from "../results";
import { Shell } from "../shell";
import { ListSelectionService } from "codeshell/services/listSelectionService";
import { Utils } from "codeshell/main";
import { Component } from '@angular/core';
import { LoadOptions, PropertyFilter, StoredLoadOptions } from '../data/listing';
import { StorageType, Stored } from "codeshell/services";


@Component({ template: '' })
export abstract class ListComponentBase extends BaseComponent {

    filter: { [key: string]: PropertyFilter } = {};
    protected abstract get Service(): EntityHttpService;

    list: any[] = [];
    totalCount: number = 0;
    pageIndex: number = 0;
    options: LoadOptions = { Showing: 10, Skip: 0 };
    storeLoadOptions = true;
    Loader?: (opts: LoadOptions) => Promise<LoadResult>;
    private _sortingClass: string | null = null;

    get CollectionId(): string | null { return null; }
    Selection: ListSelectionService | null = null;

    ngOnInit(): void {
        super.ngOnInit();

        var strd = Stored.Get("load_options", StoredLoadOptions, StorageType.Session);
        if (strd) {
            if (this.RouteData.name == strd.route) {
                this.options = strd.options;
                this.pageIndex = this.options.Skip / this.options.Showing;
            }
        }
        
        let opts = this.GetLookupOptions();
        if (opts != null) {
            this.LoadLookupsAsync(opts).then(l => {
                this.Lookups = l;
                this.Start();
            });
        } else {
            this.Start();

        }
    }

    protected LoadLookupsAsync(opts: any): Promise<any> {

        return this.Service.Get("GetListLookups", opts);
    }

    Start() {
        if (!this.IsEmbedded)
            this.LoadData();

        this.OnReady();
    }

    private _loadedOnce: boolean = false;

    async StartAsync(): Promise<ListComponentBase> {
        if (!this._loadedOnce) {
            await this.LoadData();
            this._loadedOnce = true;
        }

        return this;
    }

    protected OnReady(): void {
        (window as Window).scrollTo(0, 0);
        if (this.options.OrderProperty)
            this._sortingClass = "sorting-head-" + this.options.Direction;
    }

    PageSelected(n: number) {
        this.options.Skip = this.options.Showing * n;
        this.pageIndex = n;
        this.LoadData();
    }

    Delete(id: any) {
        if (this.Debug)
            console.log("Deleting", id)
        this.DeleteAsync(id).then(d => {
            if (d.canDelete) {
                this.NotifyTranslate("delete_success");
                this.LoadData();
            } else {
                Utils.HandleResult(d, true, true);
            }

        }).catch(d => {
            if (d != "cancelled")
                Utils.HandleError(d, true, true);
        });
    }

    async DeleteAsync(id: any): Promise<DeleteResult> {
        var c = await Shell.Main.ShowDeleteConfirm();
        if (!c) {
            return Promise.reject("cancelled");
        }
        return await this.Service.AttemptDelete(id);
    }

    protected async AttemptDelete(id: any): Promise<DeleteResult> {
        var s = await this.Service.AttemptDelete(id);

        if (s.canDelete) {
            this.LoadData();
            s.message = "delete_success";
        }
        return s;
    }

    protected LoadDataPromise(): Promise<LoadResult> {
        if (this.Loader)
            return this.Loader(this.options);

        if (this.CollectionId == null) {
            return this.Service.GetPaged("Get", this.options);
        } else {
            return this.Service.GetPaged("GetCollection/" + this.CollectionId, this.options);
        }
    }

    Clear(id?: number) {
        this.totalCount = 0;
        this.list = [];
    }

    LoadData() {
        this.options.Filters = this.stringifyFilters();

        let prom = this.LoadDataPromise();
        prom.then(e => {
            if (this.storeLoadOptions) {
                var opt = Object.assign(new LoadOptions(), this.options);
                var stord: StoredLoadOptions = { route: this.RouteData.name, options: opt }
                Stored.Set("load_options", stord, StorageType.Session);
            }
            this.ProcessResponse(e);
            this.list = e.list;
            this.totalCount = e.totalCount;
            if (this.Selection)
                this.Selection.List = this.list;
            this.AfterLoad();
        });
    }

    protected AfterLoad() { }
    protected ProcessResponse(e: LoadResult) { }

    GetFilters(): PropertyFilter[] {
        let filters: PropertyFilter[] = [];
        for (var i in this.filter) {
            if (this.filter[i].Value1 != undefined || this.filter[i].Value2 != undefined || this.filter[i].Ids.length > 0)
                filters.push(this.filter[i]);
        }
        return filters;
    }

    stringifyFilters(): string {
        var filters = this.GetFilters();
        return JSON.stringify(filters);
    }

    ClearFilter(item: PropertyFilter) {
        item.Value1 = undefined;
        item.Value2 = undefined;
        item.Ids = [];
        this.ResetPagination();
        this.LoadData();
    }

    SelectIdSingle(f: PropertyFilter, val: any) {
        if (val == 0) {
            f.Value1 = undefined;
            f.Value2 = undefined;
            f.Ids = [];
        } else {
            f.Ids = [val];
        }
        this.LoadData();
    }

    ToggleIdSingle(f: PropertyFilter, val: any) {
        let s = f.Ids.indexOf(val);
        if (s > -1)
            f.Ids.splice(s, 1);
        else {
            f.Ids = [val];
            f.Value1 = val;
        }

        this.LoadData();
    }

    ToggleId(f: PropertyFilter, id: number) {

        let s = f.Ids.indexOf(id);
        if (s > -1)
            f.Ids.splice(s, 1);
        else {
            f.Ids.push(id);
        }
        this.ResetPagination();
        this.LoadData();
    }

    IsSelected(f: PropertyFilter, id: number): boolean {
        return f.Ids.indexOf(id) > -1;
    }

    ResetPagination() {
        this.options.Skip = 0;
        this.pageIndex = 0;
    }

    HeaderSearch(term: string) {
        this.options.SearchTerm = term;
        this.ResetPagination();
        this.LoadData();
    }

    SortBy(prop: string) {
        if (prop == this.options.OrderProperty) {
            this.options.Direction = this.options.Direction == "ASC" ? "DESC" : "ASC";
        } else {
            this.options.OrderProperty = prop;
            this.options.Direction = "ASC";
        }
        this._sortingClass = "sorting-head-" + this.options.Direction;
        this.PageSelected(0);
    }

    GetHeaderClass(prop: string): string | null {
        if (this.options.OrderProperty == prop) {
            return this._sortingClass;
        }
        return null;
    }


}