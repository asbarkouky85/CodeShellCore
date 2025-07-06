import { Component, ViewChild } from "@angular/core";
import { NgForm } from '@angular/forms';
import { HomeSlideEditBase } from "@base/public/home-slides/home-slide-edit-base.component";

@Component({ templateUrl : "./home-slide-create.component.html", selector : "app-home-slide-create" })
export class HomeSlideCreate extends HomeSlideEditBase {
	
	public get CollectionId(): string | null { return null; }

	@ViewChild("Form")
	Form?:NgForm;

	ViewParams = {"AddUrl":null,"EditUrl":null,"DetailsUrl":null,"ListUrl":null,"Fields":[],"Other":{}};
	LookupOptions = null;
}