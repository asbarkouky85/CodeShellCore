import { IModel } from "./helpers/interfaces";

export * from "./helpers/interfaces";
export * from "./helpers/listItem";
export * from "./helpers/viewParams";
export * from "./helpers/tagged";
export * from "./helpers/stored";
export * from "./helpers/recursionModel";
export * from "./helpers/componentRequest";
export * from "./helpers/localizablesDTO";
export * from "./helpers/treeEventArgs";
export * from "./helpers/editablePairsDTO";
export * from "./utilities/utils";
export * from "./data/listSource";

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
    url?: string;
    tmpPath?: string;
    size?: number;
    name?: string;
    uploadId?: string ;
    mimeType?: string;
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
    innerResult: any | null;

    public static FromResponse(r: any): SubmitResult | null {
        try {
            return Object.assign(new SubmitResult, r.error);
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
    showing?: number;
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