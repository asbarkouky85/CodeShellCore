import { Component, ViewChild } from "@angular/core";
import { NgForm } from '@angular/forms';
import { EnvironmentEditBase } from "@base/environments/environment-edit-base.component";

@Component({ templateUrl : "./environment-edit-embedded.component.html", selector : "app-environment-edit-embedded" })
export class EnvironmentEditEmbedded extends EnvironmentEditBase {
	
	public get CollectionId(): string | null { return null; }

	@ViewChild("Form")
	Form?:NgForm;

	ViewParams = {"AddUrl":null,"EditUrl":null,"DetailsUrl":null,"ListUrl":null,"Fields":[],"Other":{}};
	LookupOptions = null;
}