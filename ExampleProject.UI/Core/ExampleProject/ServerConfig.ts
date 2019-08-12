import { ServerConfigBase } from "codeshell/core";

export class ServerConfig implements ServerConfigBase {
    Locale: string = "ar";
    Domain: string = "ExampleProject";

    static _instance?: ServerConfig;
    static get Instance(): ServerConfig {
        if (!this._instance) {
            this._instance = new ServerConfig();
        }
        return this._instance;
    }
}