import { Component } from "@angular/core";
import { UserEditBase } from "BaseApp/Auth/Users/UserEditBase";

@Component({ templateUrl : "./UserEdit.html", selector : "userEdit" })
export class UserEdit extends UserEditBase {
	public GetPageId() : number { return 2019082988000; }
	public get CollectionId(): string | null { return null; }
}