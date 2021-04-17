import { EntityHttpService } from "./entityHttpService";
import { LoadResultGen, LoadResult, SubmitResult } from "../results";

export abstract class NotificationServiceBase extends EntityHttpService {

    GetCount(): Promise<number> {
        return this.Get("GetCount");
    }

    SetRead(id: number): Promise<SubmitResult> {
        return this.Post("SetRead/" + id, {});
    }
}