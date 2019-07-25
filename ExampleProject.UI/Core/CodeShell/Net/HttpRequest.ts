import { RequestOptionsArgs } from "@angular/http";

export enum Methods { Get, Post, Put, Delete }

export class HttpRequest {
    public Params: RequestOptionsArgs;
    public Url: string;
    public Body: any;

    constructor(url: string, params?: number | object, body?: any) {
        this.Params = {} as RequestOptionsArgs;
        this.Url = url;
        this.Body = body;
        var add = "";
        
        if (typeof params == 'number') {
            this.Url += "/" + params;
        } else if (params instanceof Object) {
            this.Params.params = params;
        }
    }
}