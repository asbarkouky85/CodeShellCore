import { ServerEventListner } from "codeshell/http/serverEventListener";
import { Observable } from "rxjs";
import { EventEmitter } from "@angular/core";
import { ServerConfig } from "Example/ServerConfig";
import { BundlingTask } from "Example/Dtos";

export class TasksListener extends ServerEventListner {

    constructor(useServer: boolean) {
        var url = (useServer ? (ServerConfig.CurrentApp ? ServerConfig.CurrentApp.ConfigUrl : "") : "") + "/tasksHub";
        console.log("Connecting to " + url)
        super(url);
    }

    get TaskChanged(): Observable<BundlingTask> {
        return this.ObserveAs<BundlingTask>("TaskChanged", BundlingTask);
    }
}