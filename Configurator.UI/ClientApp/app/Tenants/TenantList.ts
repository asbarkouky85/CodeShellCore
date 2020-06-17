import { Component } from "@angular/core";
import { TenantListBase } from "Example/Tenants/TenantListBase";

@Component({ templateUrl : "./TenantList.html", selector : "tenantList" })
export class TenantList extends TenantListBase {
	public GetPageId() : number { return 1934264695000; }
	public get CollectionId(): string | null { return null; }
}