import { Component, ViewChild } from "@angular/core";
import { NgForm } from '@angular/forms';
import { UserListBase } from "@base/auth/users/user-list-base.component";

@Component({ templateUrl : "./user-list.component.html", selector : "app-user-list" })
export class UserList extends UserListBase {
	
	public get CollectionId(): string | null { return null; }

	@ViewChild("Form")
	Form?:NgForm;

	ViewParams = {"AddUrl":null,"EditUrl":null,"DetailsUrl":null,"ListUrl":null,"Fields":[],"Other":{}};
	LookupOptions = null;
}