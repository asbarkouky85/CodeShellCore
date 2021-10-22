import { ServerConfigBase } from "codeshell";
import { AppInfo } from "./models";

export class ServerConfig extends ServerConfigBase {
    DefaultLocale = "en";
    ApiUrl = "http://localhost:8051/";
    Apps: AppInfo[] = [];
    static CurrentApp: AppInfo | null = null;
}