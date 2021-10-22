import { Injectable } from "@angular/core";
import { PageParameterDTO } from "@base/dtos";
import { PageTypes } from "@base/pages/page-select-base.component";
import { LoadOptions } from "codeshell/data";
import { LoadResult, SubmitResult } from "codeshell/results";
import { ConfigHttpService } from "./config-http.service";

@Injectable()
export class PagesService extends ConfigHttpService {


    protected get BaseUrl(): string {
        return "/apiAction/Pages";
    }


    FindPages(tenantId: number, type: PageTypes, opts: LoadOptions): Promise<LoadResult> {
        return this.Post("FindPages", { tenantId: tenantId, type: type }, opts)
    }

    GetPagesByDomain(domainId: number, opts: LoadOptions, tenantId: any) {

        if (tenantId != 0 && tenantId != undefined)
            LoadOptions.AddFilter(opts, { FilterType: "reference", Ids: [tenantId], MemberName: "TenantId" })

        return this.Get("GetPagesByDomain?domainId=" + domainId, opts);
    }

    GetCustomizationData(id: number): Promise<any> {
        return this.Get("GetCustomizationData/" + id);
    }

    ApplyCustomization(dto: any): Promise<SubmitResult> {
        return this.Post("ApplyCustomization", dto);
    }

    GetViewParameters(id: number): Promise<PageParameterDTO[]> {
        return this.Get("GetViewParameters/" + id);
    }
}
