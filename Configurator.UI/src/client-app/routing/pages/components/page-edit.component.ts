import { Component, ViewChild } from "@angular/core";
import { NgForm } from '@angular/forms';
import { PageCreateBase } from "@base/pages/page-create-base.component";

@Component({ templateUrl : "./page-edit.component.html", selector : "app-page-edit" })
export class PageEdit extends PageCreateBase {
	
	public get CollectionId(): string | null { return null; }

	@ViewChild("Form")
	Form?:NgForm;

	ViewParams = {"AddUrl":null,"EditUrl":null,"DetailsUrl":null,"ListUrl":"/routing/pages/page-tree-list","Fields":[],"Other":{"DomainModal":"Shared/DomainTreeSelect","CustomizeLink":"routing/page-controls/page-customization"}};
	LookupOptions = {
  "NavigationGroup": "C0"
};
}