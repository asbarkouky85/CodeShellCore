import { IServerConfig } from "./IServerConfig";
import { TranslateService } from "@ngx-translate/core";
import { Router, RouteConfigLoadStart, RouteConfigLoadEnd } from "@angular/router";
import { Shell } from "./Shell";
import { Injectable } from "@angular/core";

@Injectable()
export abstract class BaseModule {
    
    constructor(trans: TranslateService, router: Router, conf: IServerConfig) {
        
        trans.setDefaultLang(conf.Locale);
        trans.use(conf.Locale);

        router.events.subscribe(event => {
            if (event instanceof RouteConfigLoadStart) {
                Shell.Main.ShowLoader = true;
            }
            if (event instanceof RouteConfigLoadEnd) {
                Shell.Main.ShowLoader = false;
            }
        });
    }
}