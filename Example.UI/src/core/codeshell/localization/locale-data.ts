import { Shell } from "codeshell/shell";
import { Stored } from "codeshell/services";
import { ServerConfigBase } from "codeshell/serverConfigBase";

export class Culture {

    private _languageData?: LocaleData;
    private static _current?: Culture;

    static get Current() {
        if (!Culture._current)
            Culture._current = new Culture();
        return Culture._current;
    }

    get Language(): string {
        if (!this._languageData) {
            this._setLocaleData();
        }
        return this._languageData.Language;
    }

    set Language(lang: string) {
        this._languageData = { Language: lang };
        Stored.Set("LocaleData", this._languageData);
    }

    private _setLocaleData() {
        let stord = Stored.Get("LocaleData", LocaleData);
        if (stord)
            this._languageData = stord;
        else {
            let conf = Shell.Injector.get<ServerConfigBase>(ServerConfigBase);
            this._languageData = { Language: conf.DefaultLocale };
            Stored.Set("LocaleData", this._languageData);
        }
    }
}

export class LocaleData {
    Language: string;
}