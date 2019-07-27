import { ServerConfigBase } from "CodeShell/core";

export class ServerConfig implements ServerConfigBase {
    GoogleKey: string = "";
    BaseURL: string = "";
    Domain: string = "";
    Locale: string = "";

    constructor() {
        let item: HTMLElement = document.getElementById("view-data") as HTMLElement;
        if (item)
            Object.assign(this, JSON.parse(item.innerHTML));
    }

    private static _config: ServerConfig;
    public static Instance: ServerConfig = new ServerConfig();
}