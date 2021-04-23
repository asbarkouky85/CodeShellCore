import { Injector, EventEmitter } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { AppBaseComponent } from "./base-components/app-base-component";
import { ServerConfigBase } from "./serverConfigBase";

export class Shell {
    private static _translate: TranslateService;
    public static get Translator(): TranslateService {
        if (Shell._translate == null)
            Shell._translate = Shell.Injector.get(TranslateService);
        return Shell._translate;
    }

    public static MainAs<T extends AppBaseComponent>(): T {
        return Shell.Main as T;
    }

    public static Injector: Injector;
    public static Main: AppBaseComponent;
    public static Server: ServerConfigBase;
    public static ViewLoaded: EventEmitter<any> = new EventEmitter<any>();

    static Word(text: string, ...params: string[]): string {
        if (!text || text.length == 0)
            return text;
        return Shell.Translator.instant('Words.' + text, ...params);
    }

    static Message(text: string, ...params: string[]): string {
        if (!text || text.length == 0)
        return text;
        return Shell.Translator.instant(text, ...params);
    }

    static Column(text: string): string {
        if (!text || text.length == 0)
            return text;
        return Shell.Translator.instant(text);
    }

    static Page(text: string): string {
        if (!text || text.length == 0)
            return text;
        return Shell.Translator.instant(text);
    }

    static Translate(...params: string[]): string[] {
        for (var i in params)
            params[i] = Shell.Translator.instant(params[i]);
        return params;
    }

    static TranslateIfNeeded(text: string) {
        if (!text)
            return "";
        return Shell.Translator.instant(text);
    }
}