import { Injectable } from "@angular/core";
import { ServerConfig } from "@base/server-config";
import { EntityHttpService } from "codeshell/http";

@Injectable()
export abstract class ConfigHttpService extends EntityHttpService {

    connectionId: string = "";
    get hostName() { return this.CurrentAppUrl; }

    get CurrentAppUrl(): string {
        if (ServerConfig.CurrentApp)
            return ServerConfig.CurrentApp.configUrl;
        return "";
    }

    protected AddCustomHeaders(heads: { [key: string]: string }) {

        if (ServerConfig.CurrentApp)
            heads["app-name"] = ServerConfig.CurrentApp.name;

        if (this.connectionId)
            heads["connection-id"] = this.connectionId;

    }
}
