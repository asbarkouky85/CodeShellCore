import { Component } from "@angular/core";
import { TestBase } from "BaseApp/Testing/TestBase";

@Component({ templateUrl : "./TestPage.html", selector : "testPage" })
export class TestPage extends TestBase {
	public GetPageId() : number { return 2013773514000; }
}