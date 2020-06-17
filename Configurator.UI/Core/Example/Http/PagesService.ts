import { ConfigHttpService } from "Example/Http/ConfigHttpService";
import { Injectable } from "@angular/core";
import { ServerConfig } from "Example/ServerConfig";
import { SubmitResult, LoadOptions, LoadResult } from "../../codeshell/helpers";
import { PageParameterDTO } from "Example/Dtos";
import { PageTypes } from "Example/Pages/PageSelectBase";

@Injectable()
export class PagesService extends ConfigHttpService {


    protected get BaseUrl(): string {
        return this.CurrentAppUrl + "/apiAction/Pages";
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
