import { Injectable } from "@angular/core";

@Injectable({providedIn:"root"})
export class ServerConfigBase {
    DefaultLocale: string = "en";
    Locale: string = "en";
    Domain: string = "Auth";
    Version?: string | null = null;
}
