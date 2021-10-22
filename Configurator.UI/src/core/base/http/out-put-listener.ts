import { LogMessage } from "@base/models";
import { ServerConfig } from "@base/server-config";
import { ServerEventListner } from "codeshell/http";
import { Observable } from "rxjs";


export class OutPutListener extends ServerEventListner {
    
    constructor(useServer: boolean = true) {
        let server = (useServer ? (ServerConfig.CurrentApp ? ServerConfig.CurrentApp.configUrl : "") : "")
        //var url = (useServer ? (ServerConfig.CurrentApp ? ServerConfig.CurrentApp.configUrl : "") : "") + "/generationHub";
        super("/generationHub", server);

    }

    get SendMessage(): Observable<LogMessage> {
        return this.ObserveAs<LogMessage>("SendMessage", LogMessage);
    }
}