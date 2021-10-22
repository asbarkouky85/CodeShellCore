import { Component, ViewChild } from "@angular/core";
import { NgForm } from '@angular/forms';
import { TenantCreateBase } from "@base/tenants/tenant-create-base.component";

@Component({ templateUrl : "./tenant-details.component.html", selector : "app-tenant-details" })
export class TenantDetails extends TenantCreateBase {
	
	public get CollectionId(): string | null { return null; }

	@ViewChild("Form")
	Form?:NgForm;

	ViewParams = {"AddUrl":null,"EditUrl":null,"DetailsUrl":null,"ListUrl":"/generations/tenants/tenant-list","Fields":[],"Other":{"OutputListener":"Generations/GenerationInofList","SelectTemplateModel":"Generations/PageCategorySelect"}};
	LookupOptions = null;
}