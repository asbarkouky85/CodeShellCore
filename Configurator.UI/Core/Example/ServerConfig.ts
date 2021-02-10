import { ServerConfigBase } from "codeshell/core";

export class AppInfo {
    Name: string = "";
    ConfigUrl: string = "";
}

export class ServerConfig implements ServerConfigBase {
    GoogleKey: string = "";
    BaseURL: string = "";
    Domain: string = "";
    Locale: string = "";
    Urls: any = {};
    Apps: AppInfo[] = [];
    static CurrentApp: AppInfo | null = null;
    constructor() {
        let item: HTMLElement = document.getElementById("view-data") as HTMLElement;
        if (item)
            Object.assign(this, JSON.parse(item.innerHTML));
    }
}