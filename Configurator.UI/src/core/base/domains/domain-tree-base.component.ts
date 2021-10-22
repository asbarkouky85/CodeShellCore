import { Component, EventEmitter, Injectable } from "@angular/core";
import { DomainsService } from "@base/http";
import { Shell } from "codeshell";
import { TreeComponentBase } from "codeshell/base-components";
import { RecursionModel, List_RunRecursively, List_FindIdRecusive } from "codeshell/recursion";

export enum DomainCountMode { Pages, Categories, All }

@Component({template:''})
export abstract class DomainTreeBase extends TreeComponentBase {
    Service = new DomainsService();

    tenantId?: number;
    CountMode: DomainCountMode = DomainCountMode.Pages;
    type?: string;
    item?: RecursionModel;
    isGeneration: boolean = false;
    UseContentCounts = true;
    UseTenantTree = false;

    ProcessRequest: EventEmitter<RecursionModel> = new EventEmitter<RecursionModel>();
    RenderRequest: EventEmitter<RecursionModel> = new EventEmitter<RecursionModel>();

    OnModalHide() {

    }

    LoadDataPromise(): Promise<RecursionModel[]> {
        if (this.UseTenantTree && this.tenantId) {
            return this.Service.GetTenantTree(this.tenantId);
        }
        return super.LoadDataPromise();
    }

    render(item: RecursionModel) {
        this.RenderRequest.emit(item);
    }

    process(item: RecursionModel) {
        this.ProcessRequest.emit(item);
    }

    LoadCounts() {
        switch (this.CountMode) {
            case DomainCountMode.Pages:
            case DomainCountMode.Categories:
                super.LoadCounts();
                break;
            case DomainCountMode.All:
                List_RunRecursively(this.List, d => {
                    d.contentCount = 0;
                    (d as any).catCount = 0;
                    d.hasContents = false;
                });
                if (this.tenantId) {
                    this.Service.PageCounters(this.tenantId).then(res => {
                        for (var i in res) {
                            var item = List_FindIdRecusive(this.List, parseInt(i));
                            if (item) {
                                item.hasContents = true;
                                item.contentCount = res[i];
                            }
                        }
                    });


                }
                this.Service.PageCategoryCounters().then(res => {
                    for (var i in res) {
                        var item = List_FindIdRecusive(this.List, parseInt(i));
                        if (item) {
                            (item as any).catCount = res[i];
                        }
                    }
                });

                break;
        }


    }

    ContentCountAsync(): Promise<{ [key: number]: number }> {

        switch (this.CountMode) {
            case DomainCountMode.Pages:
                if (this.tenantId)
                    return this.Service.PageCounters(this.tenantId);
            case DomainCountMode.Categories:
                return this.Service.PageCategoryCounters();
        }

        return super.ContentCountAsync();
    }
}