import { Component } from "@angular/core";
import { TenantCreateBase } from "Example/Tenants/TenantCreateBase";

@Component({ templateUrl : "./TenantDetails.html", selector : "tenantDetails" })
export class TenantDetails extends TenantCreateBase {
	public GetPageId() : number { return 2005463254000; }
	public get CollectionId(): string | null { return null; }
}