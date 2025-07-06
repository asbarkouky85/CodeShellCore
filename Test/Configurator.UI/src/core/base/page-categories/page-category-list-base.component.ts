import { Component } from "@angular/core";
import { PageCategoriesService } from "@base/http";
import { Shell } from "codeshell";
import { ListComponentBase } from "codeshell/base-components";
import { RecursionModel } from "codeshell/recursion";

@Component({ template: '' })
export abstract class PageCategoryListBase extends ListComponentBase {
    Service = new PageCategoriesService();

    Domain?: RecursionModel;


    LoadDataPromise() {
        if (this.Domain) {
            return this.Service.GetPageCategoryByDomain(this.Domain.id, this.options);
        }
        else {
            return this.Service.Get("Get", this.options);
        }
    }


}