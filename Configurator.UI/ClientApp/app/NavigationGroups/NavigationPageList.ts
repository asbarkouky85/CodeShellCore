import { Component } from "@angular/core";
import { NavigationPageListBase } from "Example/NavigationGroups/NavigationPageListBase";

@Component({ templateUrl : "./NavigationPageList.html", selector : "navigationPageList" })
export class NavigationPageList extends NavigationPageListBase {
	public GetPageId() : number { return 2000143913000; }
	public get CollectionId(): string | null { return null; }
}