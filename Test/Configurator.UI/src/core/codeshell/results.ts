export enum NoteType { Success, Error, Warning }

export class Result {
    success: boolean = true;
    message?: string;
    type?:NoteType;
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

export class LoadResultGen<T>{
    list: T[] = [];
}

export class TmpFileData {
    url?: string;
    tmpPath?: string;
    size?: number;
    name?: string;
    uploadId?: string;
    mimeType?: string;
}