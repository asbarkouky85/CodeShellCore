import { Component } from "@angular/core";
import { PageCategoryCreateBase } from "@base/page-categories/page-category-create-base.component";

@Component({ templateUrl : "./page-category-select.component.html", selector : "app-page-category-select" })
export class PageCategorySelect extends PageCategoryCreateBase {
	public get CollectionId(): string | null { return null; }

	ViewParams = {"AddUrl":null,"EditUrl":null,"DetailsUrl":null,"ListUrl":null,"Fields":[],"Other":{}};
	LookupOptions = {
  "Resources": "C0",
  "BaseComponent": "C0"
};
}