import { Component } from "@angular/core";
import { HomeSlideEditBase } from "Example/public/HomeSlides/HomeSlideEditBase";

@Component({ templateUrl : "./HomeSlideEdit.html", selector : "HomeSlideEdit" })
export class HomeSlideEdit extends HomeSlideEditBase {
	public GetPageId() : number { return 0; }
	public get CollectionId(): string | null { return null; }
}