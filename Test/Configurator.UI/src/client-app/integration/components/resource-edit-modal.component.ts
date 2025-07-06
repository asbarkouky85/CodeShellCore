import { Component, ViewChild } from "@angular/core";
import { NgForm } from '@angular/forms';
import { ResourceEditBase } from "@base/resources/resource-edit-base.component";

@Component({ templateUrl : "./resource-edit-modal.component.html", selector : "app-resource-edit-modal" })
export class ResourceEditModal extends ResourceEditBase {
	
	public get CollectionId(): string | null { return null; }

	@ViewChild("Form")
	Form?:NgForm;

	ViewParams = {"AddUrl":null,"EditUrl":null,"DetailsUrl":null,"ListUrl":null,"Fields":[],"Other":{}};
	LookupOptions = {
  "domains": "C0"
};
}