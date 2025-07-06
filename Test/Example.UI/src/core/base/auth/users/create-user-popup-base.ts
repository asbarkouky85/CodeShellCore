import { Component, Injectable } from "@angular/core";
import { Shell } from "codeshell";
import { EditComponentBase } from "codeshell/base-components";
import { UsersService } from "../http";

@Component({ template: '' })
export abstract class CreateUserPopupBase extends EditComponentBase {
    Service = new UsersService();
}