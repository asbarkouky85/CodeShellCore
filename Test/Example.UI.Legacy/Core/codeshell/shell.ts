import { Injector, EventEmitter } from "@angular/core";
import { SessionManager } from "./security/sessionManager";
import { TranslateService } from "@ngx-translate/core";
import { IAppComponent } from "./baseComponents";
import { ServerConfigBase } from "./serverConfigBase";

import { UserDTO } from "./security/models";



export class Shell {
    private static _translate: TranslateService;
    private static _session: SessionManager;
    public static get Translator(): TranslateService {
        if (Shell._translate == null)
            Shell._translate = Shell.Injector.get(TranslateService);
        return Shell._translate;
    }

    public static get Session(): SessionManager {
        return this._session;
    }

    public static Start(comp: IAppComponent) {
        this.Main = comp;
        this._session = new SessionManager;
        this._session.GetDeviceId();
        this._session.CheckToken();
    }

    public static MainAs<T extends IAppComponent>(): T {
        return Shell.Main as T;
    }

    public static Injector: Injector;
    public static Main: IAppComponent;
    public static Server: ServerConfigBase;
    public static ViewLoaded: EventEmitter<any> = new EventEmitter<any>();

    static Word(text: string, params?: { [index: string]: string }): string {

        if (params)
            params = Shell.Translate(params);

        return Shell.Translator.instant('Words.' + text, params);
    }

    static Message(text: string, params?: { [index: string]: string }): string {

        if (params)
            params = Shell.Translate(params);

        return Shell.Translator.instant('Messages.' + text, params);
    }

    static Column(text: string): string {
        return Shell.Translator.instant('Columns.' + text);
    }

    static Page(text: string): string {
        return Shell.Translator.instant('Pages.' + text);
    }

    static Translate(params: { [index: string]: string }): {} {
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