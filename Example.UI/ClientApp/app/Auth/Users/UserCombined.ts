import { Component } from "@angular/core";
import { TestBase } from "BaseApp/Testing/TestBase";

@Component({ templateUrl : "./UserCombined.html", selector : "userCombined" })
export class UserCombined extends TestBase {
	public GetPageId() : number { return 2018403774000; }
}