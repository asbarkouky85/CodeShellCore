import { ConfigHttpService } from "Example/Http/ConfigHttpService";
import { Injectable } from "@angular/core";
import { ServerConfig } from "Example/ServerConfig";

@Injectable()
export class DomainsService extends ConfigHttpService {

    protected get BaseUrl(): string {
        return this.CurrentAppUrl + "/apiAction/Domains";
    }

    PageCounters(tenantId: number): Promise<{ [key: number]: number }> {
        return this.Get("PageCounters/" + tenantId);
    }

    PageCategoryCounters(): Promise<{ [key: number]: number }>  {
        return this.Get("PageCategoryCounters");
    }
}
