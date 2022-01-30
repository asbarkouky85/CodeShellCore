import { Component } from "@angular/core";
import { RoleEditBase } from "BaseApp/Auth/Roles/RoleEditBase";

@Component({ templateUrl : "./RoleCreate.html", selector : "roleCreate" })
export class RoleCreate extends RoleEditBase {
	public GetPageId() : number { return 2014381971000; }
	public get CollectionId(): string | null { return null; }
}