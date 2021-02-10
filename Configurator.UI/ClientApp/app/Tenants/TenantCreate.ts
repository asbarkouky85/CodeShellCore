import { Component } from "@angular/core";
import { TenantCreateBase } from "Example/Tenants/TenantCreateBase";

@Component({ templateUrl : "./TenantCreate.html", selector : "tenantCreate" })
export class TenantCreate extends TenantCreateBase {
	public GetPageId() : number { return 2005463214000; }
	public get CollectionId(): string | null { return null; }
}