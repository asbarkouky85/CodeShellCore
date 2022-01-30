import { Component } from "@angular/core";
import { UserListBase } from "BaseApp/Auth/Users/UserListBase";

@Component({ templateUrl : "./UserListEmbed.html", selector : "userListEmbed" })
export class UserListEmbed extends UserListBase {
	public GetPageId() : number { return 2018403734000; }
}