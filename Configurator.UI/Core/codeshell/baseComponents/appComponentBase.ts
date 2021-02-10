import { Injector, ComponentFactoryResolver, ComponentRef, EventEmitter, ViewChild } from "@angular/core";
import { Router } from "@angular/router";
import { Title } from "@angular/platform-browser";

import { ToastrService } from "ngx-toastr";

import { DeleteResult, NoteType, SubmitResult, Utils } from "../helpers";
import { ServerConfigBase } from "../serverConfigBase";
import { BaseComponent } from "../baseComponents";
import { Shell } from "../shell";
import { TestLoader } from "../forms";
import { Registry } from "../utilities/registry";
import { TopBarBase } from "./topBarBase";
import { UserDTO } from "codeshell/security";

export abstract class IAppComponent {

    private _loaderTimeout: number | null = null;
    promptShow: boolean = false;
    deleteDialogShow: boolean = false;
    ConfirmDialogShow: boolean = false;
    ShowLoader: boolean = false;
    Public: boolean = false;
    headerMsg: string = "";
    MsgData?: any;
    promptMessage: string = "";
    TokenIsExpired: EventEmitter<void> = new EventEmitter<void>();
    SideBarStatus: EventEmitter<boolean> = new EventEmitter<boolean>();
    get FactoryResolver(): ComponentFactoryResolver { return Shell.Injector.get(ComponentFactoryResolver); }
    IsLoggedIn: boolean = false;
    get Config(): ServerConfigBase { return this.conf; }
    get Router(): Router { return Shell.Injector.get(Router); }

    OnDeleteConfirm: (e: Event) => void = e => { };
    OnDeleteCancel: (e: Event) => void = e => { };

    protected Toaster: ToastrService;
    abstract ModalLoader?: TestLoader;

    @ViewChild("topBar") topBar?: TopBarBase;


    constructor(inj: Injector, private title: Title, private conf: ServerConfigBase) {
        Shell.Injector = inj;
        Shell.Start(this);
        Shell.Session.LogStatus.subscribe((d: boolean) => {
            this.IsLoggedIn = d;
        });
        Shell.Session.GetUserAsync().then(d => {
            this.IsLoggedIn = true;
            this.OnStartupSessionFound(d);
        }).catch(e => this.OnStartupNoSession(e));

        this.SideBarStatus.subscribe((state: boolean) => {

            setTimeout(() => {
                if (this.topBar)
                    this.topBar.setSideBarState(state)
            }, 500);
        })

        Shell.Session.OnUserDataFailed.subscribe((error: SubmitResult) => {
            this.ShowPromptTranslate(error.message);
        })
        this.Toaster = inj.get(ToastrService);
    }

    ngOnInit() {

    }

    OnStartupSessionFound(dto: UserDTO) { }
    OnStartupNoSession(response: any) { }

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
        Shell.Session.EndSession();
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

    SetTitle(pageIdentifier: string) {
        this.title.setTitle(Shell.Page(pageIdentifier));
    }

    ClearModalLoader() {
        if (this.ModalLoader)
            this.ModalLoader.Container.clear();
    }

    private ShowMessage(type: NoteType, e: string, title?: string) {
        switch (type) {
            case NoteType.Error:
                this.Toaster.error(e, title, {
                    positionClass: "toast-top-left"
                });
                break;
            case NoteType.Success:
                this.Toaster.success(e, title, {
                    positionClass: "toast-top-left"
                });
                break;
            case NoteType.Warning:
                this.Toaster.warning(e, title, {
                    positionClass: "toast-top-left"
                });
                break;

        }
    }

}