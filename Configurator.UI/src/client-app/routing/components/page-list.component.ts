import { Component, ViewChild } from "@angular/core";
import { NgForm } from '@angular/forms';
import { PageListBase } from "@base/pages/page-list-base.component";

@Component({ templateUrl : "./page-list.component.html", selector : "app-page-list" })
export class PageList extends PageListBase {
	
	public get CollectionId(): string | null { return null; }

	@ViewChild("Form")
	Form?:NgForm;

	ViewParams = {"AddUrl":null,"EditUrl":"/routing/pages/page-edit","DetailsUrl":null,"ListUrl":null,"Fields":[],"Other":{"ControlsLink":"Routing/PageControls/PageCustomization"}};
	LookupOptions = null;
}