import { Component } from "@angular/core";
import { PageCategoryListBase } from "Example/PageCategories/PageCategoryListBase";

@Component({ templateUrl : "./PageCategoryList.html", selector : "pageCategoryList" })
export class PageCategoryList extends PageCategoryListBase {
	public GetPageId() : number { return 1935659181000; }
	public get CollectionId(): string | null { return null; }
}