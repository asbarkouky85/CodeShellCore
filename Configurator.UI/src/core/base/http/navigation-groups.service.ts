import { Injectable } from "@angular/core";
import { LoadOptions } from "codeshell/data";
import { SubmitResult } from "codeshell/results";
import { ConfigHttpService } from "./config-http.service";

@Injectable()
export class NavigationGroupsService extends ConfigHttpService {
   
  

    protected get BaseUrl(): string {
        return "/apiAction/NavigationGroups";
    }

    naveId: number = 0;
    navePageList?: any[] = [];
    GetPagesByNave(naveId: number, opts: LoadOptions) {
        return this.Get("GetPagesByNav?naveId=" + naveId, opts);
    }

    GetPageToAdd(opts: LoadOptions, tenantId: any) {
        if (tenantId != 0 && tenantId != undefined)
            LoadOptions.AddFilter(opts, { FilterType: "reference", Ids: [tenantId], MemberName: "TenantId" })
        return this.Get("GetPageToAdd", opts);
    }

    GetgetTenants() {
        return this.Get("GetTenant");
    }

    DeleteNavPage(id: number) {
        return this.Get("DeleteNavPage/" + id);
    }

    SetApplyOrder(applyOrder: any): Promise<SubmitResult> {
        debugger;
        return this.PostAs<SubmitResult>("SetApplyOrder", applyOrder);
    }

    updateNave(item: any): Promise<SubmitResult> {
        return this.PostAs<SubmitResult>("UpdateNave", item);
    }

    create(item: any): Promise<SubmitResult> {
        return this.PostAs<SubmitResult>("CreateNave", item);
    }
}
