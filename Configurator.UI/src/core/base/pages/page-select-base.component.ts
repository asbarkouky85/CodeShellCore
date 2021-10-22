import { Component } from "@angular/core";
import { PagesService } from "@base/http";
import { Shell } from "codeshell";
import { SelectComponentBase } from "codeshell/base-components";
import { LoadOptions } from "codeshell/data";
import { LoadResult } from "codeshell/results";

export enum PageTypes { All, AnyRoutable, ParameterizedRoutable, UnParameterizedRoutable, Embedded }

@Component({template:''})
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