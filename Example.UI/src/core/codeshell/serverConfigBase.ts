import { Injectable } from "@angular/core";

@Injectable({ providedIn: "root" })
export class ServerConfigBase {
    DefaultLocale: string = "en";
    Domain: string = "Auth";
    Version?: string | null = null;
    ApiUrl?: string;
    Urls: { [key: string]: string } = {};
    Production: boolean = false;
}
