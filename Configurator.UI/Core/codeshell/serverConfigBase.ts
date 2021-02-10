import { Injectable } from "@angular/core";

@Injectable()
export class ServerConfigBase {
    Locale: string = "en";
    Domain: string = "Auth";
    Version?: string | null = null;
}
