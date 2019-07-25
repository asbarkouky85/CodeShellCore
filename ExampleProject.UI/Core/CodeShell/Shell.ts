import { Injector } from "@angular/core";
import { SessionManager } from "CodeShell/Security/SessionManager";
import { TranslateService } from "@ngx-translate/core";
import { IAppComponent } from "CodeShell/IAppComponent";
import { IServerConfig } from "CodeShell/IServerConfig";
import { IAnimationContainer } from "CodeShell/Animations";



export class Shell {
    private static _translate: TranslateService;

    public static get Translator(): TranslateService {
        if (Shell._translate == null)
            Shell._translate = Shell.Injector.get(TranslateService);
        return Shell._translate;
    }

    public static get Session(): SessionManager {
        return SessionManager.Current();
    }

    public static Animations: IAnimationContainer;

    public static Injector: Injector;
    public static Main: IAppComponent;
    public static Server: IServerConfig;

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
}