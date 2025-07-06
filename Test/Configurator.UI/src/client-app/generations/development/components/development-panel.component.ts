import { Component, ViewChild } from "@angular/core";
import { NgForm } from '@angular/forms';
import { GenerationNotificationBase } from "@base/generations/generation-notification-base.component";

@Component({ templateUrl : "./development-panel.component.html", selector : "app-development-panel" })
export class DevelopmentPanel extends GenerationNotificationBase {
	
	public get CollectionId(): string | null { return null; }

	@ViewChild("Form")
	Form?:NgForm;

	ViewParams = {"AddUrl":null,"EditUrl":null,"DetailsUrl":null,"ListUrl":null,"Fields":[],"Other":{"DomainTree":"Shared/DomainTree","GenerationInofList":"Generations/GenerationInofList"}};
	LookupOptions = null;
}