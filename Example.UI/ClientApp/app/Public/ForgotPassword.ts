import { Component } from "@angular/core";
import { ForgotPasswordBase } from "BaseApp/Auth/Users/ForgotPasswordBase";

@Component({ templateUrl : "./ForgotPassword.html", selector : "forgotPassword" })
export class ForgotPassword extends ForgotPasswordBase {
	public GetPageId() : number { return 2024353205000; }
	public get CollectionId(): string | null { return null; }
}