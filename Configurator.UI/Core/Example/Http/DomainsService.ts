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

    PageCategoryCounters(): Promise<{ [key: number]: number }> {
        return this.Get("PageCategoryCounters");
    }

    UpdateFiles(name: string) {
        return this.Post("UpdateFiles", {}, { assemblyName: name });
    }

    InstallModule(assemblyName: string) {
        return this.Post("InstallModule", {}, { assemblyName: assemblyName });
    }
}
