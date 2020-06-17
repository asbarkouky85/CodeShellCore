import { Component } from "@angular/core";
import { PageCategoryEditBase } from "Example/PageCategories/PageCategoryEditBase";

@Component({ templateUrl : "./PageCategoryEdit.html", selector : "pageCategoryEdit" })
export class PageCategoryEdit extends PageCategoryEditBase {
	public GetPageId() : number { return 2005864840000; }
	public get CollectionId(): string | null { return null; }
}