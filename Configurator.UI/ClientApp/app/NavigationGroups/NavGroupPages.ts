import { Component } from "@angular/core";
import { NavGroupPagesBase } from "Example/NavigationGroups/NavGroupPagesBase";

@Component({ templateUrl : "./NavGroupPages.html", selector : "navGroupPages" })
export class NavGroupPages extends NavGroupPagesBase {
	public GetPageId() : number { return 2000136858000; }
	public get CollectionId(): string | null { return null; }
}