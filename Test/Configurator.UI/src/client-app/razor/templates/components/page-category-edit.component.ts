import { Component, ViewChild } from "@angular/core";
import { NgForm } from '@angular/forms';
import { PageCategoryEditBase } from "@base/page-categories/page-category-edit-base.component";

@Component({ templateUrl : "./page-category-edit.component.html", selector : "app-page-category-edit" })
export class PageCategoryEdit extends PageCategoryEditBase {
	
	public get CollectionId(): string | null { return null; }

	@ViewChild("Form")
	Form?:NgForm;

	ViewParams = {"AddUrl":null,"EditUrl":null,"DetailsUrl":null,"ListUrl":"/razor/templates/page-categories-tree-list","Fields":[],"Other":{}};
	LookupOptions = {
  "Resources": "C0",
  "BaseComponent": "C0",
  "layouts": "C0"
};
}