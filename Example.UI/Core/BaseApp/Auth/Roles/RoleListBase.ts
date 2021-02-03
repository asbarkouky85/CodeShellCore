import { ListComponentBase } from "codeshell/baseComponents";
import { Injectable } from "@angular/core";
import { RolesService } from "BaseApp/Auth/Http";

@Injectable()
export abstract class RoleListBase extends ListComponentBase{
	Service = new RolesService();
}