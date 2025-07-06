import { Component, ViewChild } from "@angular/core";
import { NgForm } from '@angular/forms';
import { PageParameterListBase } from "@base/page-parameters/page-parameter-list-base.component";

@Component({ templateUrl : "./page-reference-list.component.html", selector : "app-page-reference-list" })
export class PageReferenceList extends PageParameterListBase {
	
	public get CollectionId(): string | null { return null; }

	@ViewChild("Form")
	Form?:NgForm;

	ViewParams = {"AddUrl":null,"EditUrl":null,"DetailsUrl":null,"ListUrl":null,"Fields":[],"Other":{}};
	LookupOptions = null;
}