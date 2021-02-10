import { Component } from "@angular/core";
import { ResourceListBase } from "Example/Resources/ResourceListBase";

@Component({ templateUrl : "./ResourceList.html", selector : "resourceList" })
export class ResourceList extends ResourceListBase {
	public GetPageId() : number { return 2005670949000; }
	public get CollectionId(): string | null { return null; }
}