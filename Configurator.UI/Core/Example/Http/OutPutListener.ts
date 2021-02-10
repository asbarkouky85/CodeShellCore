import { ServerEventListner } from "codeshell/http/serverEventListener";
import { Observable } from "rxjs";
import { EventEmitter } from "@angular/core";
import { ServerConfig } from "Example/ServerConfig";

export class OutPutListener extends ServerEventListner {

    constructor(useServer: boolean) {
        var url = (useServer ? (ServerConfig.CurrentApp ? ServerConfig.CurrentApp.ConfigUrl : "") : "") + "/generationHub";
        super(url);
    }

    private _sendMessage?: EventEmitter<any>;
    get SendMessage(): EventEmitter<any> {
        if (!this._sendMessage) {
            this._sendMessage = new EventEmitter<any>();
            this.connection.on("SendMessage", d => this.SendMessage.emit(d));
        }
        return this._sendMessage;

    }
}