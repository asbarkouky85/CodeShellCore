﻿import { Component } from "@angular/core";
import { UserEditBase } from "BaseApp/Auth/Users/UserEditBase";

@Component({ templateUrl : "./UserCreate.html", selector : "userCreate" })
export class UserCreate extends UserEditBase {
	public GetPageId() : number { return 2018265998000; }
	public get CollectionId(): string | null { return null; }
}