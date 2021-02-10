import { Component } from "@angular/core";
import { PageSelectBase } from "Example/Pages/PageSelectBase";

@Component({ templateUrl : "./NavigationPageCreate.html", selector : "navigationPageCreate" })
export class NavigationPageCreate extends PageSelectBase {
	public GetPageId() : number { return 2000161545000; }
	public get CollectionId(): string | null { return null; }
}