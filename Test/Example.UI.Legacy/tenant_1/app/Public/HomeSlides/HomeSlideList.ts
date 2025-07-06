import { Component } from "@angular/core";
import { HomeSlideListBase } from "Example/public/HomeSlides/HomeSlideListBase";

@Component({ templateUrl : "./HomeSlideList.html", selector : "HomeSlideList" })
export class HomeSlideList extends HomeSlideListBase {
	public GetPageId() : number { return 0; }
	public get CollectionId(): string | null { return null; }
}