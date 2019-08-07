﻿import { HttpServiceBase } from "CodeShell/Net/HttpServiceBase";
import { Utils } from "CodeShell/Utilities/Utils";
import { HttpRequest, Methods } from "CodeShell/Http";
import { TmpFileData } from "CodeShell/Helpers";


export class FilesHttpService extends HttpServiceBase {
    protected BaseUrl: string;

    private ServerUrl: string;

    private _uploadId: string | null = null;

    private get UploadId(): string {
        if (this._uploadId == null)
            this._uploadId = Utils.GetIdString();
        return this._uploadId;
    }

    constructor(base: string) {
        super();
        this.BaseUrl = base + "/apiAction/Files";
        this.ServerUrl = base;
    }

    public async PostFiles(action: string, files: FileList|null, params?: number | object): Promise<TmpFileData[]> {
        let body = new FormData();
        if (files != null) {
            for (var i = 0; i < files.length; i++) {
                let s = this.UploadId + (i).toString();
                body.append(s, files[i]);
            }

            let req: HttpRequest = this.InitializeRequest(action, params, body);

            let res = await this.processAs<TmpFileData[]>(Methods.Post, req);
            for (var n in res) {
                res[n].url = Utils.Combine(this.ServerUrl, res[n].url);
            }
            return res;
        } else {
            return [];
        }
        
    }
}