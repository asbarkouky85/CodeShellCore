import { ConfigHttpService } from "Example/Http/ConfigHttpService";
import { Injectable } from "@angular/core";
import { ServerConfig } from "Example/ServerConfig";
import { LoadOptions, SubmitResult } from "../../codeshell/helpers";

@Injectable()
export class PageCategoriesService extends ConfigHttpService {

    protected get BaseUrl(): string {
        return this.CurrentAppUrl + "/apiAction/PageCategories";
    }

    GetPageCategoryByDomain(domainId: number, opts: LoadOptions) {
        return this.Get("GetPagesCategoryByDomain?domainId=" + domainId, opts);
    }

    GetTemplate(): Promise<any> {
        return this.Get("GetTemplate");
    }

    CreatPageCategory(dto: any): Promise<SubmitResult> {
        return this.PostAs<SubmitResult>("CreatPageCategory", dto);
    }

    EditPageCategory(dto: any): Promise<SubmitResult> {
        return this.PostAs<SubmitResult>("EditPageCategory", dto);
    }
}
