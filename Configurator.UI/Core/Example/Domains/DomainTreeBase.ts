import { ListComponentBase, TreeComponentBase } from "codeshell/baseComponents";
import { Injectable, EventEmitter } from "@angular/core";
import { Shell } from "codeshell/core";
import { DomainsService } from "Example/Http/DomainsService";
import { RecursionModel } from "../../codeshell/helpers";
import { C } from "@angular/core/src/render3";

export enum DomainCountMode { Pages, Categories }

@Injectable()
export abstract class DomainTreeBase extends TreeComponentBase {
    get Service(): DomainsService { return Shell.Injector.get(DomainsService); }

    tenantId?: number;
    CountMode: DomainCountMode = DomainCountMode.Pages;
    type?: string;
    item?: RecursionModel;
    isGeneration: boolean = false;

    ProcessRequest: EventEmitter<RecursionModel> = new EventEmitter<RecursionModel>();
    RenderRequest: EventEmitter<RecursionModel> = new EventEmitter<RecursionModel>();

    OnModalHide() {
        console.log('close modal');
    }

    render(item: RecursionModel) {
        this.RenderRequest.emit(item);
    }

    process(item: RecursionModel) {
        this.ProcessRequest.emit(item);
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