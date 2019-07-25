import { IServerConfig } from "CodeShell/IServerConfig";

export class ServerConfig implements IServerConfig {
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