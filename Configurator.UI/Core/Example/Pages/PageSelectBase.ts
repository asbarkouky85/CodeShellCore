import { BaseComponent, ListComponentBase, SelectComponentBase } from "codeshell/baseComponents";
import { Injectable } from "@angular/core";
import { NavigationGroupsService } from "../Http";
import { Shell } from "../../codeshell/core";
import { LoadOptions, LoadResult } from "codeshell/helpers";
import { PagesService } from "Example/Http/PagesService";

export enum PageTypes { All, AnyRoutable, ParameterizedRoutable, UnParameterizedRoutable, Embedded }

@Injectable()
export abstract class PageSelectBase extends SelectComponentBase {

    get Service(): PagesService { return Shell.Injector.get(PagesService); }

    list: any[] = [];
    model?: any = {};
    tenants: any[] = [];
    tenantId: number = 0;
    pageType: PageTypes = PageTypes.All;

    ngOnInit() {
        super.ngOnInit();
        
    }

    LoadDataAsync(opts: LoadOptions): Promise<LoadResult> {
        console.log(this.tenantId, this.pageType, opts);
        return this.Service.FindPages(this.tenantId, this.pageType, opts);
    }

    async StartAsync(): Promise<any> {
        if (!this.Source.LoadedOnce) {
            this.Source.Loader = opts => this.LoadDataAsync(opts);
            await this.Source.LoadDataAsync();
        }
        
    }

    onTableChange() {
        //this.Service.navePageList = this.Source.List.map(x => x.Tag);
    }

    tenantChanged() {
        this.LoadData();
    }
}