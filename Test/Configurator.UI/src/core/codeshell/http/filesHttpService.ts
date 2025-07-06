import { HttpServiceBase } from "./httpServiceBase";
import { Utils } from "../utilities/utils";
import { HttpRequest, Methods } from "./httpRequest";
import { TmpFileData } from "../results";


export class FilesHttpService extends HttpServiceBase {
    protected BaseUrl: string;


    private _uploadId: string | null = null;

    private get UploadId(): string {
        if (this._uploadId == null)
            this._uploadId = Utils.GetIdString();
        return this._uploadId;
    }

    constructor(base: string) {
        super(base);
        this.BaseUrl = "/apiAction/Files";
    }

    public async PostFiles(action: string, files: FileList | null, params?: number | object): Promise<TmpFileData[]> {
        let body = new FormData();
        if (files != null) {
            for (var i = 0; i < files.length; i++) {
                let s = this.UploadId + (i).toString();
                body.append(s, files[i]);
            }

            let req: HttpRequest = this.InitializeRequest(action, params, body);

            let res = await this.processAs<TmpFileData[]>(Methods.Post, req);
            for (var n in res) {
                res[n].url = Utils.Combine(this.hostName, res[n].url as string);
            }
            return res;
        } else {
            return [];
        }

    }
}