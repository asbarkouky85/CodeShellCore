import { Injectable } from "@angular/core";
import { Router, RouteConfigLoadStart, RouteConfigLoadEnd } from "@angular/router";
import { ServerConfigBase } from "./serverConfigBase";
import { Shell } from "./shell";
import { TranslationService } from "./localization/translationService";

@Injectable()
export abstract class BaseModule {
    
    constructor(trans: TranslationService, router: Router, conf: ServerConfigBase) {
        
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