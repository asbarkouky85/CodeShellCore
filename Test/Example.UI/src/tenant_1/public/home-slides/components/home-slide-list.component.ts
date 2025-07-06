import { Component, ViewChild } from "@angular/core";
import { NgForm } from '@angular/forms';
import { HomeSlideListBase } from "@base/public/home-slides/home-slide-list-base.component";

@Component({ templateUrl : "./home-slide-list.component.html", selector : "app-home-slide-list" })
export class HomeSlideList extends HomeSlideListBase {
	
	public get CollectionId(): string | null { return null; }

	@ViewChild("Form")
	Form?:NgForm;

	ViewParams = {"AddUrl":"/public/home-slides/home-slide-create","EditUrl":"/public/home-slides/home-slide-edit","DetailsUrl":null,"ListUrl":null,"Fields":[],"Other":{}};
	LookupOptions = null;
}