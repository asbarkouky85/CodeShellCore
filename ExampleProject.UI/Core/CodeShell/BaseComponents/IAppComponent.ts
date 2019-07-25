import { DeleteResult, NoteType } from "CodeShell/Helpers";
import { IServerConfig } from "CodeShell/IServerConfig";
import { BaseComponent } from "./BaseComponent";

export interface IAppComponent
{
    ShowDeleteConfirm(onConfirm: (e: Event) => Promise<DeleteResult>, onDeleteSuccess?: () => void): void;
    ShowPrompt(data: string): void;
    GetDialog(path: string): BaseComponent | null;
    GetDialogAs<T extends BaseComponent>(path: string): T | null;

    Notify(message: string, type: NoteType | undefined, title: string | undefined): void;
    NotifyTranslate(messageId: string, type: NoteType | undefined, title: string | undefined): void;

    Public: boolean;
    GetServerConfig(): IServerConfig;
    ShowLoader: boolean;
    LogOut(): void;
}