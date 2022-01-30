import { Component } from "@angular/core";
import { ChangePasswordBase } from "BaseApp/Auth/Users/ChangePasswordBase";

@Component({ templateUrl : "./ChangePassword.html", selector : "changePassword" })
export class ChangePassword extends ChangePasswordBase {
	public GetPageId() : number { return 2018402918000; }
	public get CollectionId(): string | null { return null; }
}