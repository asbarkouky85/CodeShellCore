import { Component, ViewChild } from "@angular/core";
import { NgForm } from '@angular/forms';
import { EnvironmentListBase } from "@base/environments/environment-list-base.component";

@Component({ templateUrl : "./environment-list.component.html", selector : "app-environment-list" })
export class EnvironmentList extends EnvironmentListBase {
	
	public get CollectionId(): string | null { return null; }

	@ViewChild("Form")
	Form?:NgForm;

	ViewParams = {"AddUrl":null,"EditUrl":null,"DetailsUrl":null,"ListUrl":null,"Fields":[],"Other":{"editComponent":"app-generations/environments/environment-edit-embedded"}};
	LookupOptions = null;
}