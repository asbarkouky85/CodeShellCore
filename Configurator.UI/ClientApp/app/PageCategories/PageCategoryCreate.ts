import { Component } from "@angular/core";
import { PageCategoryCreateBase } from "Example/PageCategories/PageCategoryCreateBase";

@Component({ templateUrl : "./PageCategoryCreate.html", selector : "pageCategoryCreate" })
export class PageCategoryCreate extends PageCategoryCreateBase {
	public GetPageId() : number { return 1935838902000; }
}