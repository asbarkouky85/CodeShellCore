import { Component, Injectable } from "@angular/core";
import { Shell } from "codeshell";
import { EditComponentBase } from "codeshell/base-components";
import { AccountServiceBase } from "codeshell/http";
import { UsersService } from "../http";

@Component({ template: '' })
export abstract class ForgotPasswordBase extends EditComponentBase {
    Service = new UsersService();
    get AccountService(): AccountServiceBase { return Shell.Injector.get(AccountServiceBase); }

    SubmitAsync() {
        return this.AccountService.SendResetMail(this.model);
    }
}