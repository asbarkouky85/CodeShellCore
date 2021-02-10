import { Component } from "@angular/core";
import { GenerationNotificationBase } from "Example/Generations/GenerationNotificationBase";

@Component({ templateUrl : "./GenerationNotification.html", selector : "generationNotification" })
export class GenerationNotification extends GenerationNotificationBase {
	public GetPageId() : number { return 2001961468000; }
	public get CollectionId(): string | null { return null; }
}