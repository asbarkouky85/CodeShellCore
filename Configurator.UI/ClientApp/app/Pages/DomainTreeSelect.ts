import { Component } from "@angular/core";
import { DomainTreeBase } from "Example/Domains/DomainTreeBase";

@Component({ templateUrl : "./DomainTreeSelect.html", selector : "domainTreeSelect" })
export class DomainTreeSelect extends DomainTreeBase {
	public GetPageId() : number { return 1936458151000; }
	public get CollectionId(): string | null { return null; }
}