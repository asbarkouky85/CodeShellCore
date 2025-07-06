import { Component, ViewChild } from "@angular/core";
import { NgForm } from '@angular/forms';
import { GenerationInofBase } from "@base/generations/generation-inof-base.component";

@Component({ templateUrl : "./generation-inof-list.component.html", selector : "app-generation-inof-list" })
export class GenerationInofList extends GenerationInofBase {
	
	public get CollectionId(): string | null { return null; }

	@ViewChild("Form")
	Form?:NgForm;

	ViewParams = {"AddUrl":null,"EditUrl":null,"DetailsUrl":null,"ListUrl":null,"Fields":[],"Other":{}};
	LookupOptions = null;
}