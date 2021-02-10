import { SessionManager } from "../security/sessionManager";
import { HttpClient, HttpErrorResponse } from "@angular/common/http/";
import { ServerConfigBase } from "../serverConfigBase";
import { Shell } from "../shell";
import { HttpRequest, Methods } from "./httpRequest";
import { TokenData } from "../security/Models";
import { SubmitResult } from "codeshell/helpers";
import { Utils } from "../utilities/utils";

export abstract class HttpServiceBase {
    protected abstract get BaseUrl(): string;
    protected Sessions: SessionManager;
    protected Client: HttpClient;
    protected Server: ServerConfigBase;
    
    Silent: boolean = false;
    SignalRConnctionId?: string;

    public get Headers(): any {
        let head: any = {
            "tenant-code": this.Server.Domain,
            "locale": this.Server.Locale,
            "ui-version": this.Server.Version
        };
        this.Sessions.CheckToken();
        let tok: TokenData | null = this.Sessions.GetToken();

        if (tok != null)
            head["auth-token"] = tok.Token;
        if (this.SignalRConnctionId)
            head["connection-id"] = this.SignalRConnctionId;

        this.AddCustomHeaders(head);
        return head;
    }

    protected AddCustomHeaders(data: { [key: string]: string }) { }

    constructor() {
        this.Client = Shell.Injector.get(HttpClient);
        this.Sessions = SessionManager.Current();
        this.Server = Shell.Main.Config;
    }


    protected InitializeRequest(action: string, params?: number | object, body?: any): HttpRequest {
        let url: string = this.BaseUrl + "/" + action;
        let r: HttpRequest = new HttpRequest(url, params, body);
        r.Params.headers = this.Headers;
        return r;
    }

    protected async process(method: Methods, req: HttpRequest): Promise<any> {
        var p = new Promise<any>(() => { });
        if (!this.Silent)
            Shell.Main.ShowLoading();

        switch (method) {
            case Methods.Get:
                p = this.Client.get(req.Url, req.Params).toPromise();
                break;
            case Methods.Post:
                p = this.Client.post(req.Url, req.Body, req.Params).toPromise();
                break;
            case Methods.Put:
                p = this.Client.put(req.Url, req.Body, req.Params).toPromise();
                break;
            case Methods.Delete:
                p = this.Client.delete(req.Url, req.Params).toPromise();
                break;
        }

        p.catch(e => this.OnError(e));
        p.then(e => this.OnRequestProcessed(e));
        return p;
    }

    protected processAs<T>(method: Methods, req: HttpRequest): Promise<T> {
        var p = new Promise<any>(() => { });
        if (!this.Silent)
            Shell.Main.ShowLoading();

        switch (method) {
            case Methods.Get:
                p = this.Client.get(req.Url, req.Params).toPromise();
                break;
            case Methods.Post:
                p = this.Client.post(req.Url, req.Body, req.Params).toPromise();
                break;
            case Methods.Put:
                p = this.Client.put(req.Url, req.Body, req.Params).toPromise();
                break;
            case Methods.Delete:
                p = this.Client.delete(req.Url, req.Params).toPromise();
                break;
        }
        p.catch(e => this.OnError(e));
        p.then(e => this.OnRequestProcessed(e));
        return p;
    }

    protected OnError(e: HttpErrorResponse) {

        Utils.HandleError(e);

        Shell.Main.HideLoading();
    }

    protected OnRequestProcessed(e: any) {
        Shell.Main.HideLoading();
    }
}