import { Component } from "@angular/core";
import { PageTreeListBase } from "Example/Pages/PageTreeListBase";

@Component({ templateUrl : "./PageTreeList.html", selector : "pageTreeList" })
export class PageTreeList extends PageTreeListBase {
	public GetPageId() : number { return 1935266339000; }
	public get CollectionId(): string | null { return null; }
}