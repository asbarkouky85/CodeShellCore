import { Component, ViewChild } from "@angular/core";
import { NgForm } from '@angular/forms';
import { PageSelectBase } from "@base/pages/page-select-base.component";

@Component({ templateUrl : "./navigation-page-create.component.html", selector : "app-navigation-page-create" })
export class NavigationPageCreate extends PageSelectBase {
	
	public get CollectionId(): string | null { return null; }

	@ViewChild("Form")
	Form?:NgForm;

	ViewParams = {"AddUrl":null,"EditUrl":null,"DetailsUrl":null,"ListUrl":null,"Fields":[],"Other":{}};
	LookupOptions = null;
}