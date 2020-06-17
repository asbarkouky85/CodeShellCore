import { Component } from "@angular/core";
import { ResourceEditBase } from "Example/Resources/ResourceEditBase";

@Component({ templateUrl : "./ResourceEditModal.html", selector : "resourceEditModal" })
export class ResourceEditModal extends ResourceEditBase {
	public GetPageId() : number { return 2016168499000; }
	public get CollectionId(): string | null { return null; }
}