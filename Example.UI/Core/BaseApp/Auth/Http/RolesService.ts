import { EntityHttpService } from "codeshell/http";

export class RolesService extends EntityHttpService {
    protected get BaseUrl(): string {
        return "/apiAction/Roles";
    }
}