import { Component } from "@angular/core";
import { PageCategoryCreateBase } from "Example/PageCategories/PageCategoryCreateBase";

@Component({ templateUrl : "./PageCategorySelect.html", selector : "pageCategorySelect" })
export class PageCategorySelect extends PageCategoryCreateBase {
	public GetPageId() : number { return 2005545694000; }
}