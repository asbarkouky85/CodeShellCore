import { Component } from "@angular/core";
import { NaveListBase } from "Example/NavigationGroups/NaveListBase";

@Component({ templateUrl : "./NaveList.html", selector : "naveList" })
export class NaveList extends NaveListBase {
	public GetPageId() : number { return 2000137159000; }
	public get CollectionId(): string | null { return null; }
}