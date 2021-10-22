import { Component, ViewChild } from "@angular/core";
import { NgForm } from '@angular/forms';
import { PageCreateBase } from "@base/pages/page-create-base.component";

@Component({ templateUrl : "./page-create.component.html", selector : "app-page-create" })
export class PageCreate extends PageCreateBase {
	
	public get CollectionId(): string | null { return null; }

	@ViewChild("Form")
	Form?:NgForm;

	ViewParams = {"AddUrl":null,"EditUrl":null,"DetailsUrl":null,"ListUrl":"/routing/pages/page-tree-list","Fields":[],"Other":{"DomainModal":"Shared/DomainTreeSelect","CustomizeLink":"Routing/PageControls/PageCustomization"}};
	LookupOptions = {
  "TemplatePath": "C0",
  "TenantCode": "C0",
  "Apps": "C0",
  "Resources": "C0",
  "Layout": "C0",
  "NavigationGroup": "C0",
  "Collection": "C0"
};
}