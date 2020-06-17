import { Component } from "@angular/core";
import { GenerationInofBase } from "Example/Generations/GenerationInofBase";

@Component({ templateUrl : "./GenerationInofList.html", selector : "generationInofList" })
export class GenerationInofList extends GenerationInofBase {
	public GetPageId() : number { return 2001964387000; }
	public get CollectionId(): string | null { return null; }
}