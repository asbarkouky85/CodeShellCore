import { Component } from "@angular/core";
import { ListComponentBase } from "codeshell/base-components";
import { RolesService } from "../http";

@Component({template:''})
export abstract class RoleListBase extends ListComponentBase{
	Service = new RolesService();
}