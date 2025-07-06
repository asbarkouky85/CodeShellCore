import {HttpParams} from "@angular/common/http";
import {HttpHeaders} from "@angular/common/http";

export enum Methods {Get, Post, Put, Delete}

export class RequestParams {
    headers?: HttpHeaders | {
        [header: string]: string | string[];
    };
    observe?: 'body';
    params?: HttpParams | any;
    reportProgress?: boolean;
    responseType?:any
    withCredentials?: boolean;
}

export class HttpRequest {
    public Params: RequestParams = new RequestParams;
    public Url: string;
    public Body: any;

    constructor(url: string, params?: number | object, body?: any) {
        this.Params = new RequestParams;
        this.Url = url;
        this.Body = body;

        if (typeof params == 'number') {
            this.Url += "/" + params;
        } else if (params) {
            this.Params.params = params;
        }
    }
}