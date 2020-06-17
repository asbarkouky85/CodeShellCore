import { Injectable } from "@angular/core";
import { UsersService } from "./../Http/UsersService";
import { Shell } from "codeshell/core";
import { ListComponentBase } from "codeshell/baseComponents";

@Injectable()
export abstract class UserListBase extends ListComponentBase {
    Service = new UsersService;
}