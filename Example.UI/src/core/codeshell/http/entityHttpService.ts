import { Injectable } from "@angular/core";
import { HttpRequest, Methods } from "./httpRequest";
import { LoadResult, SubmitResult, DeleteResult } from "../results";
import { HttpServiceBase } from "./httpServiceBase";
import { ListItem } from "../data/list-item";
import { DTO } from '../data/models';
import { LoadOptions } from '../data/listing';
import { LocalizablesDTO } from '../localization/models';

@Injectable()
export abstract class EntityHttpService extends HttpServiceBase {

    public IsUnique(property: string, id: number | undefined | null, value: any) {
        let req: HttpRequest = this.InitializeRequest("IsUnique", { Property: property, Id: (id ? id : 0), Value: value });
        return this.processAs<boolean>(Methods.Get, req);
    }

    public GetEditLookups(opt: any): Promise<{ [key: string]: any[] }> {
        return this.Get("edit-lookups", opt);
    }

    public GetListLookups(opt: any): Promise<{ [key: string]: any[] }> {
        return this.Get("list-lookups", opt);
    }

    SetActive(id: number, state: boolean): Promise<SubmitResult> {
        return this.Update("SetActive", { id: id, stateBool: state });
    }

    public GetSingle(id: number): Promise<DTO<any>> {
        return this.GetAs<DTO<any>>(id.toString());
    }

    public GetPaged(action: string, opts: LoadOptions): Promise<LoadResult> {
        let req: HttpRequest = this.InitializeRequest(action, opts);
        return this.processAs<LoadResult>(Methods.Get, req);
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