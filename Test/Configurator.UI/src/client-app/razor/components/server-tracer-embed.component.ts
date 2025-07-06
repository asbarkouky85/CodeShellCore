import { Component, ViewChild } from "@angular/core";
import { NgForm } from '@angular/forms';
import { GenerationInofBase } from "@base/generations/generation-inof-base.component";

@Component({ templateUrl : "./server-tracer-embed.component.html", selector : "app-server-tracer-embed" })
export class ServerTracerEmbed extends GenerationInofBase {
	
	public get CollectionId(): string | null { return null; }

	@ViewChild("Form")
	Form?:NgForm;

	ViewParams = {"AddUrl":null,"EditUrl":null,"DetailsUrl":null,"ListUrl":null,"Fields":[],"Other":{}};
	LookupOptions = null;
}