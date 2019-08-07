import { SessionManager } from "CodeShell/Security/SessionManager";
import { Http } from "@angular/http";
import { IServerConfig } from "CodeShell/IServerConfig";
import { Shell } from "CodeShell/Shell";
import { HttpRequest, Methods } from "CodeShell/Net/HttpRequest";
import { TokenData } from "CodeShell/Security/Models";
import "rxjs/add/operator/map";

export abstract class HttpServiceBase {
    protected abstract get BaseUrl(): string;
    protected Sessions: SessionManager;
    protected Client: Http;
    protected Server: IServerConfig;
    Silent: boolean = false;

    public get Headers(): any {
        let head: any = {
            "tenant-code": this.Server.Domain,
            "locale": this.Server.Locale
        };

        let tok: TokenData | null = this.Sessions.GetToken();

        if (tok != null)
            head["auth-token"] = tok.Token;
        else if (this.Sessions.IsLoggedIn)
            Shell.Main.TokenIsExpired.emit();
        return head;
    }

    constructor() {
        this.Client = Shell.Injector.get(Http);
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
        Shell.Main.ShowLoader = true;

        switch (method) {
            case Methods.Get:
                p = this.Client.get(req.Url, req.Params).map(d => d.json()).toPromise();
                break;
            case Methods.Post:
                
                p = this.Client.post(req.Url, req.Body, req.Params).map(d => d.json()).toPromise();
                break;
            case Methods.Put:
                p = this.Client.put(req.Url, req.Body, req.Params).map(d => d.json()).toPromise();
                break;
            case Methods.Delete:
                p = this.Client.delete(req.Url, req.Params).map(d => d.json()).toPromise();
                break;
        }

        p.catch(e => this.OnError(e));
        p.then(e => this.OnRequestProcessed(e));
        return p;
    }

    protected processAs<T>(method: Methods, req: HttpRequest): Promise<T> {
        var p = new Promise<any>(() => { });
        if (!this.Silent)
        Shell.Main.ShowLoader = true;
        
        switch (method) {
            case Methods.Get:
                p = this.Client.get(req.Url, req.Params).map(d => d.json()).toPromise();
                break;
            case Methods.Post:
                p = this.Client.post(req.Url, req.Body, req.Params).map(d => d.json()).toPromise();
                break;
            case Methods.Put:
                p = this.Client.put(req.Url, req.Body, req.Params).map(d => d.json()).toPromise();
                break;
            case Methods.Delete:
                p = this.Client.delete(req.Url, req.Params).map(d => d.json()).toPromise();
                break;
        }
        p.catch(e => this.OnError(e));
        p.then(e => this.OnRequestProcessed(e));
        return p;
    }

    protected OnError(e: any) {
        try {
            console.error(e.json());
        } catch (ex) {
            console.error(e);
        }
        Shell.Main.ShowLoader = false;
    }

    protected OnRequestProcessed(e: any) {
       Shell.Main.ShowLoader = false;
    }
}