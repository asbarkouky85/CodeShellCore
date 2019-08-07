import { IModel } from "./Helpers/Interfaces";

export * from "CodeShell/Helpers/Interfaces";
export * from "CodeShell/Helpers/ListItem";
export * from "CodeShell/Helpers/ViewParams";
export * from "CodeShell/Helpers/Tagged";
export * from "CodeShell/Helpers/Stored";
export * from "CodeShell/Helpers/RecursionModel";

export interface IHasLocation {
    longitude?: number;
    latitude?: number;
}

export interface IDTO {
    entity: any;
}

export class DTO<T>{
    entity: T = {} as T;
}

export class TmpFileData {
    url: string = "";
    tmpPath: string = "";
    size: number = 0;
    name: string = "";
    uploadId: string = "";
    mimeType: string = "";
}

export class Result {
    success: boolean = true;
    message?: string;
    type: NoteType = NoteType.Success;

}

export class SubmitResult {

    public data: { [key: string]: any } = {};
    public code: number = 0;
    public message: string = "";
    public exceptionMessage: string = "";
    public stackTrace: string[] = [];
    public affectedRows: number = 0;

    public static FromResponse(r: any): SubmitResult | null {
        try {
            return Object.assign(new SubmitResult, r.json());
        } catch (e) {
            return null;
        }
    }
}

export class DeleteResult extends SubmitResult {
    public canDelete: boolean = false;
    tableName: string | null = null;
}

export class LoadResult {
    totalCount: number = 0;
    list: any[] = [];
}

export class PropertyFilter {
    MemberName: string = "";
    FilterType: string = "";
    Value1?: string;
    Value2?: string;
    Ids: any[] = [];
}

export class LoadOptions {
    Showing: number = 0;
    Skip: number = 0;
    OrderProperty?: string;
    Direction?: string;
    SearchTerm?: string;
    Filters?: string;
    AsLookup?: boolean;

    static AddFilter(opts: LoadOptions, fil: PropertyFilter) {
        let arr: PropertyFilter[];

        if (!opts.Filters) {
            opts.Filters = "";
            arr = [];
        }
        else {
            let f = JSON.parse(opts.Filters);
            arr = Object.assign(new Array<PropertyFilter>(), f);
        }
        arr.push(fil);
        opts.Filters = JSON.stringify(arr);
    }
}

export enum NoteType { Success, Error, Warning }