import { Component, ViewChild } from "@angular/core";
import { NgForm } from '@angular/forms';
import { NavigationPageListBase } from "@base/navigation-groups/navigation-page-list-base.component";

@Component({ templateUrl : "./navigation-page-list.component.html", selector : "app-navigation-page-list" })
export class NavigationPageList extends NavigationPageListBase {
	
	public get CollectionId(): string | null { return null; }

	@ViewChild("Form")
	Form?:NgForm;

	ViewParams = {"AddUrl":null,"EditUrl":null,"DetailsUrl":null,"ListUrl":null,"Fields":[],"Other":{"select_pages_modal":"Routing/NavigationPageCreate"}};
	LookupOptions = null;
}