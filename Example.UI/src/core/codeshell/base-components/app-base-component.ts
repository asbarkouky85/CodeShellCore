import { Injector, ComponentFactoryResolver, ComponentRef, EventEmitter, ViewChild, Component } from "@angular/core";
import { Router, RouterOutlet } from "@angular/router";
import { Title } from "@angular/platform-browser";

import { ToastrService } from "ngx-toastr";

import { DeleteResult, NoteType, SubmitResult } from "../results";
import { ServerConfigBase } from "../serverConfigBase";
import { BaseComponent } from ".";
import { Shell } from "../shell";
import { Registry } from "../moldster/registry";
import { TopBarBase } from "./top-bar-base-component";
import { SessionManager, UserDTO } from "codeshell/security";
import { HttpClient } from "@angular/common/http";
import { ComponentLoader } from "codeshell/directives";
import { TranslationService } from 'codeshell/localization/translationService';
import { Culture } from "codeshell/localization/locale-data";

@Component({ template: '' })
export abstract class AppBaseComponent {

    private _loaderTimeout: any = null;

    ShowLoader: boolean = false;
    IsLoggedIn: boolean = false;

    deleteDialogShow: boolean = false;
    confirmTitle?: string;
    confirmMessage?: string;
    RestrictLang?: string;
    Locale: string = "ar";

    promptShow: boolean = false;
    promptMessage: string = "";

    ShowNav: boolean = true;


    TokenIsExpired: EventEmitter<void> = new EventEmitter<void>();
    SideBarStatus: EventEmitter<boolean> = new EventEmitter<boolean>();
    get FactoryResolver(): ComponentFactoryResolver { return Shell.Injector.get(ComponentFactoryResolver); }
    Config: ServerConfigBase;

    get Router(): Router { return Shell.Injector.get(Router); }

    OnDeleteConfirm: (e: Event) => void = e => { };
    OnDeleteCancel: (e: Event) => void = e => { };

    //protected Toaster: ToastrService;
    private _translation: TranslationService;
    protected RedirectToLogin: boolean = true;
    abstract ModalLoader?: ComponentLoader;

    @ViewChild("topBar") topBar?: TopBarBase;

    constructor(private _injecto: Injector, private title: Title) {

        Shell.Injector = _injecto;
        Shell.Main = this;

        this._translation = _injecto.get(TranslationService);
        this.Locale = Culture.Current.Language;
        
        this.ChangeLangAsync(this.Locale, true);

        SessionManager.StartApp();
        SessionManager.Current.LogStatus.subscribe((d: boolean) => {
            this.IsLoggedIn = d;
            this.OnLogStatusChanged(d);
        });
        SessionManager.Current.GetUserAsync().then(d => {
            this.IsLoggedIn = true;
            this.OnStartupSessionFound(d);
        }).catch(e => {

            this.OnStartupNoSession(e)
        });

        this.SideBarStatus.subscribe((state: boolean) => {

            setTimeout(() => {
                if (this.topBar)
                    this.topBar.setSideBarState(state)
            }, 500);
        })

        SessionManager.Current.OnUserDataFailed.subscribe((error: SubmitResult) => {
            this.ShowPromptTranslate(error.message);
        })
        this.Config = Shell.Injector.get(ServerConfigBase);
        //this.Toaster = inj.get(ToastrService);
    }

    ngOnInit() {
        // if (this.RestrictLang && this.Config.Locale != this.RestrictLang) {
        //     this.ChangeLangAsync(this.RestrictLang).then(e => location.reload());
        // }
    }

    GetMainUrl(): string {
        return '/';
    }

    OnStartupSessionFound(dto: UserDTO) {

    }

    OnStartupNoSession(response: any) {
        if (this.RedirectToLogin)
            this.Router.navigateByUrl("/login");
    }

    OnLogStatusChanged(res: boolean) { }

    ChangeLangAsync(code: string, startup: boolean = false): Promise<Object> {

        this._translation = this._injecto.get(TranslationService);
        this._translation.use(code);
        this._translation.setDefaultLang("ar");
        Culture.Current.Language = code;
        this.Locale = code;
        if (!startup)
            location.reload();
        return Promise.resolve({});
    }

    ShowPrompt(data: string): void {
        this.promptMessage = data;
        this.promptShow = true;
    }

    ShowPromptTranslate(data: string): void {
        this.promptMessage = Shell.Message(data);
        this.promptShow = true;
    }

    ShowLoading() {
        this.ShowLoader = true;
        if (this._loaderTimeout)
            clearTimeout(this._loaderTimeout);
    }

    HideLoading() {
        if (this._loaderTimeout)
            clearTimeout(this._loaderTimeout);

        this._loaderTimeout = setTimeout(() => {
            this.ShowLoader = false;
        }, 800);

    }

    GetDialogAs<T extends BaseComponent>(path: string): ComponentRef<T> | null {
        
        if (path == null)
            return null;
        let e = Registry.Get(path);

        if (e && this.ModalLoader) {

            var ref = this.ModalLoader.CreateComponent(e) as ComponentRef<T>;
            this.ModalLoader.UseComponent(ref);
            return ref;
        }
        return null;
    }

    OnPromptOk() {
        this.promptShow = false;
    }

    LogOut(): void {
        SessionManager.Current.EndSession();
        Shell.Injector.get(Router).navigate(["/"]);
    }

    ShowDeleteConfirmLocal(onConfirm: () => void) {
        this.deleteDialogShow = true;
        this.OnDeleteConfirm = e => {
            this.deleteDialogShow = false;
            onConfirm()
        };
    }



    ShowDeleteConfirm(): Promise<boolean> {
        this.deleteDialogShow = true;

        this.confirmTitle = Shell.Word("Delete");
        this.confirmMessage = Shell.Message("delete_confirm_message");
        return new Promise((res, rej) => {
            this.OnDeleteConfirm = e => {
                this.deleteDialogShow = false;
                res(true);
            };
            this.OnDeleteCancel = e => {
                this.deleteDialogShow = false;
                res(false);
            };
        });
    }

    Confirm(message: string, translate: boolean = true, title?: string): Promise<void> {
        this.deleteDialogShow = true;
        return new Promise((res, rej) => {

            if (title)
                this.confirmTitle = translate ? Shell.Word(title) : title;
            else
                this.confirmTitle = "";
            this.confirmMessage = translate ? Shell.Message(message) : message;

            this.OnDeleteConfirm = e => {

                this.deleteDialogShow = false;
                res();
            };
            this.OnDeleteCancel = e => {
                this.deleteDialogShow = false;
                rej();
            };
        });
    }

    Notify(message: string, type?: NoteType, title?: string) {
        type = type == undefined ? NoteType.Success : type;
        let typ = NoteType[type].toString();
        title = title ? title : typ;
        this.ShowMessage(type, message, Shell.Word(title));
    }

    NotifyTranslate(messageId: string, type?: NoteType, title?: string) {
        type = type == undefined ? NoteType.Success : type;
        let typ = NoteType[type].toString();
        title = title ? title : typ;
        this.ShowMessage(type, Shell.Message(messageId), Shell.Word(title));
    }

    SetTitle(pageIdentifier: string, translate = true) {
        if (translate)
            this.title.setTitle(Shell.Page(pageIdentifier));
        else
            this.title.setTitle(pageIdentifier);
    }


    ClearModalLoader() {
        if (this.ModalLoader)
            this.ModalLoader.Container.clear();
    }

    private ShowMessage(type: NoteType, e: string, title?: string) {
        // switch (type) {
        //     case NoteType.Error:
        //         this.Toaster.error(e, title, {
        //             positionClass: "toast-top-left"
        //         });
        //         break;
        //     case NoteType.Success:
        //         this.Toaster.success(e, title, {
        //             positionClass: "toast-top-left"
        //         });
        //         break;
        //     case NoteType.Warning:
        //         this.Toaster.warning(e, title, {
        //             positionClass: "toast-top-left"
        //         });
        //         break;

        // }
    }

}