import { Component, ViewChild } from "@angular/core";
import { NgForm } from '@angular/forms';
import { PageCategoryListBase } from "@base/page-categories/page-category-list-base.component";

@Component({ templateUrl : "./page-category-list.component.html", selector : "app-page-category-list" })
export class PageCategoryList extends PageCategoryListBase {
	
	public get CollectionId(): string | null { return null; }

	@ViewChild("Form")
	Form?:NgForm;

	ViewParams = {"AddUrl":null,"EditUrl":"/razor/templates/page-category-edit","DetailsUrl":null,"ListUrl":null,"Fields":[],"Other":{"EditModal":null}};
	LookupOptions = null;
}