import { Component } from "@angular/core";
import { RoleEditBase } from "BaseApp/Auth/Roles/RoleEditBase";

@Component({ templateUrl : "./RoleEdit.html", selector : "roleEdit" })
export class RoleEdit extends RoleEditBase {
	public GetPageId() : number { return 2019082885000; }
	public get CollectionId(): string | null { return null; }
}