import { Component, ViewChild } from "@angular/core";
import { NgForm } from '@angular/forms';
import { CustomTextListBase } from "@base/localization/custom-text-list-base.component";

@Component({ templateUrl : "./custom-text-list.component.html", selector : "app-custom-text-list" })
export class CustomTextList extends CustomTextListBase {
	
	public get CollectionId(): string | null { return null; }

	@ViewChild("Form")
	Form?:NgForm;

	ViewParams = {"AddUrl":null,"EditUrl":null,"DetailsUrl":null,"ListUrl":null,"Fields":[],"Other":{}};
	LookupOptions = null;
}