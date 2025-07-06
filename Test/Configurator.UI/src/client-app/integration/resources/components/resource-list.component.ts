import { Component, ViewChild } from "@angular/core";
import { NgForm } from '@angular/forms';
import { ResourceListBase } from "@base/resources/resource-list-base.component";

@Component({ templateUrl : "./resource-list.component.html", selector : "app-resource-list" })
export class ResourceList extends ResourceListBase {
	
	public get CollectionId(): string | null { return null; }

	@ViewChild("Form")
	Form?:NgForm;

	ViewParams = {"AddUrl":null,"EditUrl":null,"DetailsUrl":null,"ListUrl":null,"Fields":[],"Other":{"AddModal":"Integration/ResourceModal","EditModal":"Integration/ResourceEditModal"}};
	LookupOptions = null;
}