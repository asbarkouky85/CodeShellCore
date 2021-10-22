import { Component } from "@angular/core";
import { PagesService } from "@base/http";
import { Shell } from "codeshell";
import { ListComponentBase } from "codeshell/base-components";
import { RecursionModel } from "codeshell/recursion";
import { LoadResult } from "codeshell/results";

@Component({ template: '' })
export abstract class PageListBase extends ListComponentBase {
    Service = new PagesService();

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

        if (this.Domain != undefined)
            this.renderModel.NameChain = this.Domain.nameChain;

        this.renderModel.TenantId = this.tenantId;
        this.renderModel.Lazy = true;
        this.Service.Silent = true;
    }
}