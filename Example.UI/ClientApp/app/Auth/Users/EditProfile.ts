import { Component } from "@angular/core";
import { UserEditBase } from "BaseApp/Auth/Users/UserEditBase";

@Component({ templateUrl : "./EditProfile.html", selector : "editProfile" })
export class EditProfile extends UserEditBase {
	public GetPageId() : number { return 2018272984000; }
	public get CollectionId(): string | null { return null; }
}