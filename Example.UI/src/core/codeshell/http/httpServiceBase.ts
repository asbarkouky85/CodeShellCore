import { HttpClient, HttpErrorResponse } from "@angular/common/http/";
import { ServerConfigBase } from "../serverConfigBase";
import { Shell } from "../shell";
import { HttpRequest, Methods } from "./httpRequest";
import { TokenData } from "../security/Models";
import { SubmitResult } from "../results";
import { Utils } from "../utilities/utils";
import { TokenStorage } from '../security/tokenStorage';
import { Culture } from "codeshell/localization/locale-data";

export abstract class HttpServiceBase {
    protected abstract get BaseUrl(): string;

    protected TokenStorage: TokenStorage;
    protected Client: HttpClient;
    protected Server: ServerConfigBase;

    Silent: boolean = false;
    SignalRConnctionId?: string;
    ServiceKey?: string;

    private _baseUrl?: string;

    public get Headers(): any {
        let head: any = {
            "locale": Culture.Current.Language,
        };
        if (this.Server.Version) {
            head['ui-version'] = this.Server.Version;
        }

        if (this.Server.Domain) {
            head["tenant-code"] = this.Server.Domain;
        }

        let tok: TokenData | null = this.TokenStorage.LoadToken();

        if (tok != null)
            head["auth-token"] = tok.Token;
        if (this.SignalRConnctionId)
            head["connection-id"] = this.SignalRConnctionId;

        this.AddCustomHeaders(head);
        return head;
        return {};
    }

    protected AddCustomHeaders(data: { [key: string]: string }) { }

    constructor() {
        this.Client = Shell.Injector.get(HttpClient);
        //this.Sessions = SessionManager.Current;
        this.TokenStorage = Shell.Injector.get(TokenStorage);
        this.Server = Shell.Injector.get(ServerConfigBase);

    }

    private get _base() {
        if (!this._baseUrl) {
            this._baseUrl = "";
            if (this.ServiceKey && this.Server.Urls[this.ServiceKey]) {
                this._baseUrl = this.Server.Urls[this.ServiceKey];
            } else if (this.Server.ApiUrl) {
                this._baseUrl = this.Server.ApiUrl;
            }
        }
        return this._baseUrl;
    }

    public Get(action: string, params?: number | object): Promise<any> {
        let req: HttpRequest = this.InitializeRequest(action, params);
        return this.process(Methods.Get, req);
    }

    public GetAsHtml(action: string, params?: number | object): Promise<any> {
        let req: HttpRequest = this.InitializeRequest(action, params);
        if (req.Params.headers) {
            req.Params.responseType = "text/html";
        }
        return this.process(Methods.Get, req);
    }

    public Post(action: string, body: any, params?: number | object): Promise<any> {
        let req: HttpRequest = this.InitializeRequest(action, params, body);
        return this.process(Methods.Post, req);
    }

    public Put(action: string, body: any, params?: number | object): Promise<any> {
        let req: HttpRequest = this.InitializeRequest(action, params, body);
        return this.process(Methods.Put, req);
    }

    public Delete(action: string, id: number): Promise<any> {
        let req: HttpRequest = this.InitializeRequest(action, id);
        return this.process(Methods.Delete, req);
    }

    public GetAs<T>(action: string, params?: number | object): Promise<T> {
        let req: HttpRequest = this.InitializeRequest(action, params);
        return this.processAs<T>(Methods.Get, req);
    }

    public PostAs<T>(action: string, body: any, params?: number | object): Promise<T> {
        let req: HttpRequest = this.InitializeRequest(action, params, body);
        return this.processAs<T>(Methods.Post, req);
    }

    public Save(action: string, body: any, params?: number | object): Promise<SubmitResult> {
        let req: HttpRequest = this.InitializeRequest(action, params, body);
        return this.processAs<SubmitResult>(Methods.Post, req);
    }


    protected InitializeRequest(action: string, params?: number | object, body?: any): HttpRequest {
        let url: string = this._base + this.BaseUrl + "/" + action;
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