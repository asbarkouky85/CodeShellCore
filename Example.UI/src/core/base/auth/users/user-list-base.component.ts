import { Component } from "@angular/core";
import { ListComponentBase } from "codeshell/base-components";
import { UsersService } from "../http";

@Component({ template: '' })
export abstract class UserListBase extends ListComponentBase {
    Service = new UsersService;
}