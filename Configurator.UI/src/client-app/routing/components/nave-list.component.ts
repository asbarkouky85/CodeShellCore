import { Component, ViewChild } from "@angular/core";
import { NgForm } from '@angular/forms';
import { NaveListBase } from "@base/navigation-groups/nave-list-base.component";

@Component({ templateUrl : "./nave-list.component.html", selector : "app-nave-list" })
export class NaveList extends NaveListBase {
	
	public get CollectionId(): string | null { return null; }

	@ViewChild("Form")
	Form?:NgForm;

	ViewParams = {"AddUrl":null,"EditUrl":null,"DetailsUrl":null,"ListUrl":null,"Fields":[],"Other":{}};
	LookupOptions = null;
}