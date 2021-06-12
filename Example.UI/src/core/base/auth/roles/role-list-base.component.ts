import { Injectable } from "@angular/core";
import { ListComponentBase } from "codeshell/base-components";
import { RolesService } from "../http";

@Injectable()
export abstract class RoleListBase extends ListComponentBase{
	Service = new RolesService();
}