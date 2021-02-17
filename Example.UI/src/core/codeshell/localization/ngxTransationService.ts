import { TranslationService } from "./translationService";
import { Injectable } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";

@Injectable()
export class NgxTranslationService implements TranslationService {
    constructor(private service: TranslateService) { }

    setDefaultLang(loc: string): void {
        this.service.setDefaultLang(loc);
    }
    use(loc: string): void {
        this.service.use(loc);
    }
}