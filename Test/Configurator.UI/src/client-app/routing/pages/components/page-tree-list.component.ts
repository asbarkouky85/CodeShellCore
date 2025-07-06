import { Component, ViewChild } from "@angular/core";
import { NgForm } from '@angular/forms';
import { PageTreeListBase } from "@base/pages/page-tree-list-base.component";

@Component({ templateUrl : "./page-tree-list.component.html", selector : "app-page-tree-list" })
export class PageTreeList extends PageTreeListBase {
	
	public get CollectionId(): string | null { return null; }

	@ViewChild("Form")
	Form?:NgForm;

	ViewParams = {"AddUrl":"/routing/pages/page-create","EditUrl":null,"DetailsUrl":null,"ListUrl":null,"Fields":[{"Name":"No_of_Teams","Type":"Number"}],"Other":{"DomainTree":"Shared/DomainTree","PageList":"Routing/PageList","UseDefault":"P1","MainComponentUrl":null}};
	LookupOptions = null;
}