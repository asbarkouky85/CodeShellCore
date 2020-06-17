import { Injectable, ViewChild } from "@angular/core";
import { ListItem, Tagged, DTO, SubmitResult } from "codeshell/helpers";
import { Shell } from "codeshell/core";

import { UsersService } from "../Http";
import { User, Perm } from "../Models";
import { PrivilegeService } from "../Services/PrivilegeService";
import { DTOEditComponentBase } from "codeshell/baseComponents";
import { FilesHttpService } from "codeshell/http";

@Injectable()
export abstract class UserEditBase extends DTOEditComponentBase {

    Files = new FilesHttpService("");
    Service = new UsersService;
    photo?: string;
    model: DTO<User> = new DTO<User>();
    Privileges: Perm[] = [];
    ResourceActions: ListItem[] = [];
    Priv: PrivilegeService = new PrivilegeService([], [], [], 0);
    PreventCreateSubmit: boolean = false;
    isEditProfile: boolean = false;

    DefaultModel() {
        var mod = { entity: new User() };
        mod.entity.tenantId = Shell.Session.User.tenantId;
        return mod;
    }

    protected async LoadLookupsAsync(opts: any): Promise<any> {
        let l: any = await super.LoadLookupsAsync(opts);

        if (l.roles)
            l.roles = Tagged.CastList(l.roles);

        return l;
    }

    protected async GetModelFromServerAsync(id: number): Promise<any> {
        
        if (this.isEditProfile) {
            id = Shell.Session.User.id;
        }
        this.model = await super.GetModelFromServerAsync(id);

        for (var i in this.model.entity.roles)
            this.model.entity.roles[i] = ListItem.FromDB(this.model.entity.roles[i]);

        return this.model;
    }


    protected StartComponent() {
        this.isEditProfile = this.GetParameterAsBoolean("EditProfile");
        console.log("a7a");
        if (this.isEditProfile) {
            this.IsNew = false;
            var id = Shell.Session.User.id;
            this.Fill(id);
        } else {
            super.StartComponent();
        }
        
    }

    Upload = (files: FileList) => this.Files.PostFiles("Upload",files);

    protected OnReady() {

        if (this.Lookups.roles) {

            this.Lookups.roles = Tagged.JoinLists({
                Source: this.model.entity.roles,
                Data: this.Lookups.roles,
                Comparer: (d, s) => d.id == s.roleId,
                CreateNew: d => {
                    return { roleId: d.id, userId: this.model.entity.id };
                }
            });
        }

        this.Priv = new PrivilegeService([], [], [], 0);
        if (this.Lookups.domains != undefined) {

            this.Service.GetUserRole(this.model.entity.id).then(res => {
                this.model.entity.role = res;

                this.Privileges = [];
                this.ResourceActions = [];
                for (let priv of this.model.entity.role.roleResources) {
                    this.Privileges.push(Object.assign(new Perm, priv));
                }
                for (let priv of this.model.entity.role.roleResourceActions) {
                    var s: ListItem = Object.assign(new ListItem, priv);
                    s.selected = true;
                    this.ResourceActions.push(s);
                }
                this.Priv = new PrivilegeService(this.Lookups.domains, this.Privileges, this.ResourceActions, res.id);
            });
        }
    }

    

    SubmitAsync(): Promise<SubmitResult> {
        if (this.PreventCreateSubmit && this.IsNew) {
            this.Show = false;
            return Promise.resolve(new SubmitResult());
        }

        if (this.model.entity.role != undefined) {
            this.model.entity.role.roleResourceActions = ListItem.GetChangedItems(this.ResourceActions);
            this.model.entity.role.roleResources = ListItem.GetChangedItems(this.Privileges);
        }

        if (this.IsNew) {
            this.model.entity.tenantId = Shell.Session.User.tenantId;
        }
        return super.SubmitAsync();
    }

}