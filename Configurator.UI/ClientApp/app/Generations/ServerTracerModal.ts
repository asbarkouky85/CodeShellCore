import { Component } from "@angular/core";
import { GenerationInofBase } from "Example/Generations/GenerationInofBase";

@Component({ templateUrl : "./ServerTracerModal.html", selector : "serverTracerModal" })
export class ServerTracerModal extends GenerationInofBase {
	public GetPageId() : number { return 2006549195000; }
	public get CollectionId(): string | null { return null; }
}