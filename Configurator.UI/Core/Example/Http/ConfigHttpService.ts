import { EntityHttpService } from "codeshell/http";
import { Injectable } from "@angular/core";

import { ServerConfig } from "../ServerConfig";

@Injectable()
export abstract class ConfigHttpService extends EntityHttpService {

    connectionId: string = "";

    get CurrentAppUrl(): string {
        if (ServerConfig.CurrentApp)
            return ServerConfig.CurrentApp.ConfigUrl;
        return "";
    }

    protected AddCustomHeaders(heads: { [key: string]: string }) {

        if (ServerConfig.CurrentApp)
            heads["app-name"] = ServerConfig.CurrentApp.Name;
        
        if (this.connectionId)
            heads["connection-id"] = this.connectionId;

    }
}
