import { Component } from "@angular/core";
import { RoleListBase } from "BaseApp/Auth/Roles/RoleListBase";

@Component({ templateUrl : "./RoleList.html", selector : "roleList" })
export class RoleList extends RoleListBase {
	public GetPageId() : number { return 2014381811000; }
	public get CollectionId(): string | null { return null; }
}