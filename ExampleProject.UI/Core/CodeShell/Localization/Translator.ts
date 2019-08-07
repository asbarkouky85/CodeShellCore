import { Observable } from "rxjs/Observable";
import "rxjs/add/observable/of";
import { TranslateLoader } from "@ngx-translate/core";
import { LocaleLoader } from "CodeShell/Localization/LocaleLoader";

export class Translator extends TranslateLoader {

    private static Loaders: { [id: string]: LocaleLoader } = {};

    static SetLoaders(loaders: { [id: string]: LocaleLoader }) {
        
        Translator.Loaders = loaders;
    }

    static GetTranslator(dom?: string): TranslateLoader {
        return new Translator();
    }

    getTranslation(lang: string): Observable<any> {
        let res: any = {};
        
        if (Translator.Loaders[lang] != undefined) {
            res = Translator.Loaders[lang].Load();
        } else {
            res = {
                Columns: {},
                Words: {},
                Messages: {},
                Pages: {}
            }
        }
        return Observable.of(res);
    }
}