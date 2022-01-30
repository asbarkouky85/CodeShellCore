import { Injectable } from "@angular/core";

import { Shell } from "codeshell/core";
import { ListItem } from "codeshell/helpers";
import { DTOEditComponentBase } from "codeshell/baseComponents";

import { RolesService } from "./../Http";
import { Perm } from "./../Models";
import { PrivilegeService } from "./../Services/PrivilegeService";

@Injectable()
export abstract class RoleEditBase extends DTOEditComponentBase {

    Service = new RolesService();

    public Privileges: Perm[] = [];
    public ResourceActions: Perm[] = [];
    public Priv: PrivilegeService = new PrivilegeService([], [], [], 0);

    DefaultModel(): any {
        return {
            entity: {
                roleResources: [],
                roleResourceActions: []
            }
        };
    }

    protected OnReady() {

        for (let priv of this.model.entity.roleResources) {
            this.Privileges.push(Object.assign(new Perm, priv));
        }
        for (let priv of this.model.entity.roleResourceActions) {
            this.ResourceActions.push(Object.assign(new ListItem, priv));
        }
        
        this.Priv = new PrivilegeService(this.Lookups.resourcesByDomain, this.Privileges, this.ResourceActions, this.model.entity.id);

    }

    Submit() {
        this.model.entity.roleResources = ListItem.GetChangedItems(this.Privileges);
        this.model.entity.roleResourceActions = ListItem.GetChangedItems(this.ResourceActions);
        super.Submit();
    }
}