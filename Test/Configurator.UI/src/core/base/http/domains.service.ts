import { Injectable } from "@angular/core";
import { RecursionModel } from "codeshell/recursion";
import { ConfigHttpService } from "./config-http.service";


@Injectable()
export class DomainsService extends ConfigHttpService {

    protected get BaseUrl(): string {
        return "/apiAction/Domains";
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

    GetTenantTree(id:number):Promise<RecursionModel[]>{
        return this.Get("GetTenantTree/"+id);
    }

    GetCategoriesTree():Promise<RecursionModel[]>{
        return this.Get("GetCategoriesTree");
    }
}
