import { Component } from "@angular/core";
import { PageListBase } from "Example/Pages/PageListBase";

@Component({ templateUrl : "./PageList.html", selector : "pageList" })
export class PageList extends PageListBase {
	public GetPageId() : number { return 1935345212000; }
	public get CollectionId(): string | null { return null; }
}