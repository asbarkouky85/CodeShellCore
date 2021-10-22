import { Component, ViewChild } from "@angular/core";
import { NgForm } from '@angular/forms';
import { TenantCreateBase } from "@base/tenants/tenant-create-base.component";

@Component({ templateUrl : "./tenant-create.component.html", selector : "app-tenant-create" })
export class TenantCreate extends TenantCreateBase {
	
	public get CollectionId(): string | null { return null; }

	@ViewChild("Form")
	Form?:NgForm;

	ViewParams = {"AddUrl":null,"EditUrl":null,"DetailsUrl":null,"ListUrl":"/generations/tenants/tenant-list","Fields":[],"Other":{"OutputListener":"app-generations/generation-inof-list","SelectTemplateModel":"Generations/PageCategorySelect"}};
	LookupOptions = null;
}