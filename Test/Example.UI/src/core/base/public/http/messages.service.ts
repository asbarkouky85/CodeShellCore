import { EntityHttpService } from "codeshell/http";
import { Injectable } from "@angular/core";

@Injectable()
export class MessagesService extends EntityHttpService {

    protected get BaseUrl(): string {
        return "/apiAction/Messages";
    }

}
