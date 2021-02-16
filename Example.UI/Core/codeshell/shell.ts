﻿import { Injector, EventEmitter } from "@angular/core";
import { SessionManager } from "./security/sessionManager";
//import { TranslateService } from "@ngx-translate/core";
import { IAppComponent } from "./base-components/appComponentBase";
import { ServerConfigBase } from "./serverConfigBase";
import { LocalizationService } from '@abp/ng.core';

export class Shell {
    private static _translate: LocalizationService;
    private static _session: SessionManager;
    public static get Translator(): LocalizationService {
        if (Shell._translate == null)
            Shell._translate = Shell.Injector.get(LocalizationService);
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

    static Word(text: string, ...params: string[]): string {
        return Shell.Translator.instant('Words.' + text, ...params);
    }

    static Message(text: string, ...params: string[]): string {

        return Shell.Translator.instant(text, ...params);
    }

    static Column(text: string): string {
        return Shell.Translator.instant(text);
    }

    static Page(text: string): string {
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