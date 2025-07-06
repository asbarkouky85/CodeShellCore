import { Component } from "@angular/core";
import { HomeSlideEditBase } from "Example/public/HomeSlides/HomeSlideEditBase";

@Component({ templateUrl : "./HomeSlideCreate.html", selector : "HomeSlideCreate" })
export class HomeSlideCreate extends HomeSlideEditBase {
	public GetPageId() : number { return 0; }
	public get CollectionId(): string | null { return null; }
}