import { Component, ViewChild } from "@angular/core";
import { NgForm } from '@angular/forms';
import { PageControlListBase } from "@base/page-controls/page-control-list-base.component";

@Component({ templateUrl : "./customize-app.component.html", selector : "app-customize-app-component" })
export class CustomizeAppComponent extends PageControlListBase {
	
	public get CollectionId(): string | null { return null; }

	@ViewChild("Form")
	Form?:NgForm;

	ViewParams = {"AddUrl":null,"EditUrl":null,"DetailsUrl":null,"ListUrl":null,"Fields":[],"Other":{"PageSelector":null,"MainComponentCustomize":"true"}};
	LookupOptions = {
  "Layout": "C0",
  "Accessability": "C0",
  "Collection": "C0"
};
}