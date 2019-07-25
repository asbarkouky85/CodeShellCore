import { Injector, ComponentFactoryResolver, ComponentRef, EventEmitter } from "@angular/core";
import { Router } from "@angular/router";
import { Title } from "@angular/platform-browser";

import { ToastrService } from "ngx-toastr";

import { DeleteResult, NoteType } from "CodeShell/Helpers";
import { IServerConfig } from "CodeShell/IServerConfig";
import { BaseComponent } from "CodeShell/BaseComponents";
import { Shell } from "CodeShell/Shell";
import { TestLoader } from "CodeShell/Directives/TestLoader";
import { Registry } from "CodeShell/Utilities/Registry";

export abstract class IAppComponent {

    promptShow: boolean = false;
    deleteDialogShow: boolean = false;
    ShowLoader: boolean = false;
    Public: boolean = false;

    promptMessage: string = "";
    TokenIsExpired: EventEmitter<void> = new EventEmitter<void>();

    get FactoryResolver(): ComponentFactoryResolver { return Shell.Injector.get(ComponentFactoryResolver); }
    get IsLoggedIn(): boolean { return Shell.Session.IsLoggedIn; }
    get Config(): IServerConfig { return this.conf; }
    get Router(): Router { return Shell.Injector.get(Router); }

    OnDeleteConfirm: (e: Event) => void = e => { };

    protected Toaster: ToastrService;
    abstract ModalLoader?: TestLoader;

    constructor(inj: Injector, private title: Title, private conf: IServerConfig) {
        Shell.Injector = inj;
        this.Toaster = inj.get(ToastrService);
        this.TokenIsExpired.subscribe(() => this.LogOut());
        Shell.Session.GetDeviceId();
    }

    ShowPrompt(data: string): void {
        this.promptMessage = data;
        this.promptShow = true;
    }

    ShowPromptTranslate(data: string): void {
        this.promptMessage = Shell.Message(data);
        this.promptShow = true;
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

    OnDeleteCancel(e: Event) {
        this.deleteDialogShow = false;
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

    ShowDeleteConfirm(onConfirm: (e: Event) => Promise<DeleteResult>, onDeleteSuccess?: () => void): void {
        this.deleteDialogShow = true;
        this.OnDeleteConfirm = e => {
            onConfirm(e).then(v => {
                this.deleteDialogShow = false;
                if (v.message != null) {
                    var mess = Shell.Message(v.message, v.data);
                    if (v.canDelete) {
                        this.Notify(mess, NoteType.Success, undefined);
                        if (onDeleteSuccess)
                            onDeleteSuccess();
                    } else {
                        this.Notify(mess, NoteType.Error, undefined)
                    }
                }
            }).catch(e => {
                this.deleteDialogShow = false;
                let res: DeleteResult = Object.assign(new DeleteResult, e.json());
                let tableName = ""
                if (res.tableName)
                    tableName = Shell.Word(res.tableName);
                let mess = Shell.Message("entity_has_children", { "p0": tableName });
                this.Notify(mess, NoteType.Error, undefined);
            })
        };
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
            this.ModalLoader.Clear();
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