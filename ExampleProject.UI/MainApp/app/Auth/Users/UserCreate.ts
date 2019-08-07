import { Component } from "@angular/core";
import { UserEditBase } from "ExampleProject/Auth/Users/UserEditBase";

@Component({ templateUrl : "./UserCreate.html", selector : "userCreate" })
export class UserCreate extends UserEditBase {
	public GetPageId() : number { return 1914671555000; }
	public get CollectionId(): string | null { return null; }
}