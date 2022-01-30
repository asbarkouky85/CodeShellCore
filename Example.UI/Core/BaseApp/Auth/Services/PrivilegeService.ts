import { Perm } from "../Models";
import { ListItem } from "codeshell/helpers";

export class PrivilegeService {

    Domains: any[];
    Privileges: Perm[];
    ResourceActions: ListItem[];

    constructor(domains: any[], privileges: Perm[], resourceActions: ListItem[], roleId: number) {
        this.Domains = domains;
        this.Privileges = privileges;
        this.ResourceActions = resourceActions;
        let s = true;
        for (let domain of domains) {
            domain.Selected = s;
            s = false;
            for (let resource of domain.resources) {
                let item = privileges.find(e => e.resourceId == resource.id);
                resource.available = item != null;

                if (item == null) {
                    item = Object.assign(
                        new Perm, {
                            roleId: roleId,
                            resourceId: resource.id,
                            state: "Detached"
                        });
                }

                for (let action of resource.actions)
                {
                    let ac = resourceActions.find(e => (e as any).resourceActionId == action.id);
                    if (ac == null) {
                        ac = Object.assign(new ListItem, {
                            resourceActionId: action.id,
                            roleId: roleId,
                            state: "Detached"
                        });
                    } else {
                        ac.selected = true;
                    }
                    action.Tag = ac;
                    
                }
                resource.Tag = item;
            }
        }
        
    }

    public ItemChanged(r: any) {
        let priv: Perm = r.Tag;
        if (!r.available) {
            priv.SetAll(false);
            priv.RemoveFrom(this.Privileges);
        } else {
            if (priv.state == "Detached" || priv.state == "Removed") {
                priv.SetAll(true);
                priv.AddTo(this.Privileges);
            } else {

                priv.SetModified();
            }
        }
        
    }

    ActionChanged(item: ListItem) {
        
        if (item.selected) {
            item.AddTo(this.ResourceActions);
        } else {
            item.RemoveFrom(this.ResourceActions);
        }
        
    }

    public Select(ob: any) {
        for (let domain of this.Domains) {
            domain.Selected = false;
        }
        ob.Selected = true;
    }
}