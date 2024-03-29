import { ServerConfigBase } from "codeshell";
import { AppInfo } from "./models";

export class ServerConfig extends ServerConfigBase {
    DefaultLocale = "en";
    ApiUrl = "https://localhost:4451/";
    Apps: AppInfo[] = [];
    static CurrentApp: AppInfo | null = null;
}