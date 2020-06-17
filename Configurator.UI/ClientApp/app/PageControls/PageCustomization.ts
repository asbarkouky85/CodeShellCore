import { Component } from "@angular/core";
import { PageControlListBase } from "Example/PageControls/PageControlListBase";

@Component({ templateUrl : "./PageCustomization.html", selector : "pageCustomization" })
export class PageCustomization extends PageControlListBase {
	public GetPageId() : number { return 2000956596000; }
	public get CollectionId(): string | null { return null; }
}