import { Component, ViewChild } from "@angular/core";
import { NgForm } from '@angular/forms';
import { PageControlListBase } from "@base/page-controls/page-control-list-base.component";

@Component({ templateUrl : "./page-customization.component.html", selector : "app-page-customization" })
export class PageCustomization extends PageControlListBase {
	
	public get CollectionId(): string | null { return null; }

	@ViewChild("Form")
	Form?:NgForm;

	ViewParams = {"AddUrl":null,"EditUrl":null,"DetailsUrl":null,"ListUrl":"/routing/pages/page-tree-list","Fields":[],"Other":{"PageSelector":"Routing/NavigationPageCreate","MainComponentCustomize":null}};
	LookupOptions = {
  "Layout": "C0",
  "Accessability": "C0",
  "Collection": "C0"
};
}