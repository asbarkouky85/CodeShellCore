import { Component, ViewChild } from "@angular/core";
import { NgForm } from '@angular/forms';
import { NavGroupPagesBase } from "@base/navigation-groups/nav-group-pages-base.component";

@Component({ templateUrl : "./nav-group-pages.component.html", selector : "app-nav-group-pages" })
export class NavGroupPages extends NavGroupPagesBase {
	
	public get CollectionId(): string | null { return null; }

	@ViewChild("Form")
	Form?:NgForm;

	ViewParams = {"AddUrl":null,"EditUrl":null,"DetailsUrl":null,"ListUrl":null,"Fields":[],"Other":{"NaveList":"Routing/NaveList","NavigationPageList":"Routing/NavigationPageList"}};
	LookupOptions = null;
}