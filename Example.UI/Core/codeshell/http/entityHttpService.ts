import { Injectable } from "@angular/core";
import { HttpRequest, Methods } from "./httpRequest";
import { LoadResult, LoadOptions, SubmitResult, DeleteResult, DTO } from "../helpers";
import { HttpServiceBase } from "./httpServiceBase";
import { LocalizablesDTO, ListItem } from "../helpers";

@Injectable()
export abstract class EntityHttpService extends HttpServiceBase {

    public Get(action: string, params?: number | object): Promise<any> {
        let req: HttpRequest = this.InitializeRequest(action, params);
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

    public IsUnique(property: string, id: number | undefined | null, value: any) {
        let req: HttpRequest = this.InitializeRequest("IsUnique", { Property: property, Id: (id ? id : 0), Value: value });
        return this.processAs<boolean>(Methods.Get, req);
    }

    public GetSingle(id: number): Promise<DTO<any>> {
        return this.GetAs<DTO<any>>("GetSingle/" + id);
    }

    public GetPaged(action: string, opts: LoadOptions): Promise<LoadResult> {
        let req: HttpRequest = this.InitializeRequest(action, opts);
        return this.processAs<LoadResult>(Methods.Get, req);
    }

    public Save(action: string, body: any, params?: number | object): Promise<SubmitResult> {
        //debugger;
        let req: HttpRequest = this.InitializeRequest(action, params, body);
        return this.processAs<SubmitResult>(Methods.Post, req);
    }

    public Update(action: string, body: any, params?: number | object): Promise<SubmitResult> {
        let req: HttpRequest = this.InitializeRequest(action, params, body);
        return this.processAs<SubmitResult>(Methods.Put, req);
    }

    public AttemptDelete(id: number): Promise<DeleteResult> {
        let req: HttpRequest = this.InitializeRequest("Delete", id);
        return this.processAs<DeleteResult>(Methods.Delete, req);
    }

    public async GetLocalizationData(id: number): Promise<{ [key: string]: LocalizablesDTO }> {
        var data = await this.GetAs<{ [key: string]: LocalizablesDTO }>("GetLocalizationData/" + id);
        for (var i in data)
            data[i] = ListItem.FromDB_GEN(LocalizablesDTO, data[i]);
        return data;
    }

    public SetLocalizationData(id: number, data: { [key: string]: LocalizablesDTO }): Promise<SubmitResult> {
        return this.PostAs<SubmitResult>("SetLocalizationData/" + id, data);
    }

}