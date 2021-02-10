import { Component } from "@angular/core";
import { ResourceEditBase } from "Example/Resources/ResourceEditBase";

@Component({ templateUrl : "./ResourceModal.html", selector : "resourceModal" })
export class ResourceModal extends ResourceEditBase {
	public GetPageId() : number { return 2005670894000; }
	public get CollectionId(): string | null { return null; }
}