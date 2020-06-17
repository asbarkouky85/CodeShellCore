import { Component } from "@angular/core";
import { PageCreateBase } from "Example/Pages/PageCreateBase";

@Component({ templateUrl : "./PageEdit.html", selector : "pageEdit" })
export class PageEdit extends PageCreateBase {
	public GetPageId() : number { return 1936544141000; }
	public get CollectionId(): string | null { return null; }
}