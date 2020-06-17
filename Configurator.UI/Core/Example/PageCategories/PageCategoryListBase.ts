import { ListComponentBase } from "codeshell/baseComponents";
import { Injectable } from "@angular/core";
import { PageCategoriesService } from "Example/Http";
import { Shell } from "codeshell/core";

import { RecursionModel, ComponentRequest } from "../../codeshell/helpers";
import { PageCategoryEditBase } from "./PageCategoryEditBase";

@Injectable()
export abstract class PageCategoryListBase extends ListComponentBase{
    get Service(): PageCategoriesService { return Shell.Injector.get(PageCategoriesService); }

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