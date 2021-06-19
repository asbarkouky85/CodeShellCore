import { ServerConfigBase } from "codeshell";

export class ServerConfig extends ServerConfigBase {
    ApiUrl = "http://localhost:8040";
    Production = false;
    DefaultLocale = "en";
}