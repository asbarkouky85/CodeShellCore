import { BundlingTask } from "@base/dtos";
import { ServerConfig } from "@base/server-config";
import { ServerEventListner } from "codeshell/http";
import { Observable } from "rxjs";

export class TasksListener extends ServerEventListner {

    
    constructor(server: string) {
        super("/tasksHub", server);
    }

    get TaskChanged(): Observable<BundlingTask> {
        return this.ObserveAs<BundlingTask>("TaskChanged", BundlingTask);
    }

  
}