import { Component } from "@angular/core";
import { PageCategoriesTreeListBase } from "Example/PageCategories/PageCategoriesTreeListBase";

@Component({ templateUrl : "./PageCategoriesTreeList.html", selector : "pageCategoriesTreeList" })
export class PageCategoriesTreeList extends PageCategoriesTreeListBase {
	public GetPageId() : number { return 1935659471000; }
	public get CollectionId(): string | null { return null; }
}