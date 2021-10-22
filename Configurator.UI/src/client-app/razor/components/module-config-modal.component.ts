import { Component, ViewChild } from "@angular/core";
import { NgForm } from '@angular/forms';
import { ModulesConfigBase } from "@base/page-categories/modules-config-base.component";

@Component({ templateUrl : "./module-config-modal.component.html", selector : "app-module-config-modal" })
export class ModuleConfigModal extends ModulesConfigBase {
	
	public get CollectionId(): string | null { return null; }

	@ViewChild("Form")
	Form?:NgForm;

	ViewParams = {"AddUrl":null,"EditUrl":null,"DetailsUrl":null,"ListUrl":null,"Fields":[],"Other":{"server_tracer":"app-razor/server-tracer-embed"}};
	LookupOptions = {
  "modules": "C0"
};
}