import { Injectable } from "@angular/core";
import { LoadOptions } from "codeshell/data";
import { SubmitResult } from "codeshell/results";
import { ConfigHttpService } from "./config-http.service";

@Injectable()
export class PageCategoriesService extends ConfigHttpService {

    protected get BaseUrl(): string {
        return "/apiAction/PageCategories";
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
