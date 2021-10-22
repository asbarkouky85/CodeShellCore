import { Component, ViewChild } from "@angular/core";
import { NgForm } from '@angular/forms';
import { DomainTreeBase } from "@base/domains/domain-tree-base.component";

@Component({ templateUrl : "./domain-tree-select.component.html", selector : "app-domain-tree-select" })
export class DomainTreeSelect extends DomainTreeBase {
	
	public get CollectionId(): string | null { return null; }

	@ViewChild("Form")
	Form?:NgForm;

	ViewParams = {"AddUrl":null,"EditUrl":null,"DetailsUrl":null,"ListUrl":null,"Fields":[],"Other":{}};
	LookupOptions = null;
}