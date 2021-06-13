import { ServerConfigBase } from "codeshell";

export class ServerConfig extends ServerConfigBase {
    ApiUrl: "";
    Production: boolean = true;
    DefaultLocale: string = "en";
}