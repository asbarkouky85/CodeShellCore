import { ListComponentBase } from "codeshell/baseComponents";
import { Injectable } from "@angular/core";
import { PagesService } from "Example/Http";
import { Shell } from "codeshell/core";
import { RecursionModel, ComponentRequest, NoteType, LoadResult } from "../../codeshell/helpers";
import { Router } from "@angular/router";

@Injectable()
export abstract class PageListBase extends ListComponentBase {
    get Service(): PagesService { return Shell.Injector.get(PagesService); }

    Domain?: RecursionModel | null;
    tenantId?: number | null;
    renderModel: any = {};
    previewUrl?: string;

    LoadDataPromise() {
        if (!this.IsEmbedded)
            return super.LoadDataPromise();
        if (this.Domain && this.tenantId) {
            return this.Service.GetPagesByDomain(this.Domain.id, this.options, this.tenantId);
        } else if (this.tenantId) {
            return this.Service.Get("Get?tenantId=" + this.tenantId, this.options);
        }
        return Promise.resolve(new LoadResult());
    }

    render(model: any) {
        console.log(model);
        if (this.Domain != undefined)
            this.renderModel.NameChain = this.Domain.nameChain;

        this.renderModel.TenantId = this.tenantId;
        this.renderModel.Lazy = true;
        this.Service.Silent = true;
    }
}