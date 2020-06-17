import { Component } from "@angular/core";
import { PageCreateBase } from "Example/Pages/PageCreateBase";

@Component({ templateUrl : "./PageCreate.html", selector : "pageCreate" })
export class PageCreate extends PageCreateBase {
	public GetPageId() : number { return 1936059787000; }
	public get CollectionId(): string | null { return null; }
}