import { Component } from "@angular/core";
import { DomainTreeBase } from "Example/Domains/DomainTreeBase";

@Component({ templateUrl : "./DomainTree.html", selector : "domainTree" })
export class DomainTree extends DomainTreeBase {
	public GetPageId() : number { return 1935259991000; }
	public get CollectionId(): string | null { return null; }
}