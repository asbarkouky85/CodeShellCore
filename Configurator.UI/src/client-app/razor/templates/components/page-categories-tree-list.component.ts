import { Component, ViewChild } from "@angular/core";
import { NgForm } from '@angular/forms';
import { PageCategoriesTreeListBase } from "@base/page-categories/page-categories-tree-list-base.component";

@Component({ templateUrl : "./page-categories-tree-list.component.html", selector : "app-page-categories-tree-list" })
export class PageCategoriesTreeList extends PageCategoriesTreeListBase {
	
	public get CollectionId(): string | null { return null; }

	@ViewChild("Form")
	Form?:NgForm;

	ViewParams = {"AddUrl":null,"EditUrl":null,"DetailsUrl":null,"ListUrl":null,"Fields":[],"Other":{"DomainTree":"Shared/DomainTree","PageCategoryList":"Razor/PageCategoryList","CreateModal":"Razor/PageCategoryCreate","ModulesModal":"Razor/ModuleConfigModal"}};
	LookupOptions = null;
}