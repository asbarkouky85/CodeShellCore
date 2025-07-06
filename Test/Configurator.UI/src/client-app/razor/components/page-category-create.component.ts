import { Component } from "@angular/core";
import { PageCategoryCreateBase } from "@base/page-categories/page-category-create-base.component";

@Component({ templateUrl : "./page-category-create.component.html", selector : "app-page-category-create" })
export class PageCategoryCreate extends PageCategoryCreateBase {
	public get CollectionId(): string | null { return null; }

	ViewParams = {"AddUrl":null,"EditUrl":null,"DetailsUrl":null,"ListUrl":"/razor/templates/page-categories-tree-list","Fields":[],"Other":{}};
	LookupOptions = {
  "Resources": "C0",
  "BaseComponent": "C0"
};
}