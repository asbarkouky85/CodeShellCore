import { Component } from "@angular/core";
import { ChangePasswordBase } from "BaseApp/Auth/Users/ChangePasswordBase";

@Component({ templateUrl : "./ChangePassword.html", selector : "changePassword" })
export class ChangePassword extends ChangePasswordBase {
	public GetPageId() : number { return 2016369759000; }
}