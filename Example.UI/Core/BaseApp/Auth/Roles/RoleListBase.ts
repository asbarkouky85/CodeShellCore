import { ListComponentBase } from "codeshell/baseComponents";
import { Injectable } from "@angular/core";
import { RolesService } from "../Http";
import { Shell } from "codeshell/core";

@Injectable()
export abstract class RoleListBase extends ListComponentBase {
    Service = new RolesService();
}