import { Component, ViewChild } from "@angular/core";
import { NgForm } from '@angular/forms';
import { TenantListBase } from "@base/tenants/tenant-list-base.component";

@Component({ templateUrl : "./tenant-list.component.html", selector : "app-tenant-list" })
export class TenantList extends TenantListBase {
	
	public get CollectionId(): string | null { return null; }

	@ViewChild("Form")
	Form?:NgForm;

	ViewParams = {"AddUrl":"/generations/tenants/tenant-create","EditUrl":null,"DetailsUrl":"/generations/tenants/tenant-details","ListUrl":null,"Fields":[],"Other":{"ServerTrace":"Shared/ServerTracerModal"}};
	LookupOptions = null;
}