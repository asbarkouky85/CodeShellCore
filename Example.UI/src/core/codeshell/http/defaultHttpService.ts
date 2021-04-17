import { EntityHttpService } from "./entityHttpService";

export class DefaultHttpService extends EntityHttpService {
    protected BaseUrl: string;

    constructor() {
        
        super();
        this.BaseUrl = "";
    }

    public SetBase(base: string)
    {
        this.BaseUrl = base;
    }
}