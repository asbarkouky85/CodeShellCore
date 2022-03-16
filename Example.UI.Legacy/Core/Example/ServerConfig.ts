import { ServerConfigBase } from "codeshell/core";

export class ServerConfig implements ServerConfigBase {
    GoogleKey: string = "";
	DefaultLocale: string = "";
    Version?: string | null | undefined;
    BaseURL: string = "";
    Domain: string = "";
    Locale: string = "";
	Urls : any = {};

    constructor() {
        let item: HTMLElement = document.getElementById("view-data") as HTMLElement;
        if (item)
            Object.assign(this, JSON.parse(item.innerHTML));
    }
}