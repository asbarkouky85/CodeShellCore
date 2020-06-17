﻿import { Component } from "@angular/core";
import { UserListBase } from "BaseApp/Auth/Users/UserListBase";

@Component({ templateUrl : "./UserList.html", selector : "userList" })
export class UserList extends UserListBase {
	public GetPageId() : number { return 2013762020000; }
	public get CollectionId(): string | null { return null; }
}