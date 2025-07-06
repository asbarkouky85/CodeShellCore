import { TranslateLoader } from "@ngx-translate/core";
import { LocaleLoader } from "./localeLoader";
import { Observable, of } from "rxjs";

export class Translator extends TranslateLoader {

    private static Loaders: { [id: string]: LocaleLoader } = {};

    static SetLoaders(loaders: { [id: string]: LocaleLoader }) {

        Translator.Loaders = loaders;
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
        return of(res);
    }
}