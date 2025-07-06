import { Injectable } from "@angular/core";
import { EntityHttpService } from "codeshell/http";

@Injectable()
export class PageCategoriesTreeListService extends EntityHttpService {

    protected get BaseUrl(): string {
        return "/apiAction/PageCategoriesTreeList";
    }

}
