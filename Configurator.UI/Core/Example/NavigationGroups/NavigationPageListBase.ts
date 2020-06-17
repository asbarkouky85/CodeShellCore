import { ListComponentBase } from "codeshell/baseComponents";
import { Injectable } from "@angular/core";
import { NavigationGroupsService } from "Example/Http";
import { Shell } from "codeshell/core";
import { LoadOptions, ComponentRequest, NoteType, LoadResult, ListItem } from "../../codeshell/helpers";
import { PageSelectBase, PageTypes } from "Example/Pages/PageSelectBase";

@Injectable()
export abstract class NavigationPageListBase extends ListComponentBase {
    get Service(): NavigationGroupsService { return Shell.Injector.get(NavigationGroupsService); }

    tenantId?: number;
    navigationGroupId?: number;
    options: LoadOptions = { Showing: 0, Skip: 0 }
    applyOrder: any = {};


    async  LoadDataPromise(): Promise<LoadResult> {
        if (this.tenantId) {
            LoadOptions.AddFilter(this.options, { FilterType: "reference", Ids: [this.tenantId], MemberName: "TenantId" })

        } else {
            this.options.Filters = "[]";

        }
        var res = await this.Service.GetPagesByNave(this.Service.naveId, this.options);
        res.list = ListItem.Convert(res.list);
        return res;
    }

    ngOnInit() {
        var tenantId = localStorage.getItem("tenantId");
        if (tenantId != null) {
            this.tenantId = parseInt(tenantId);
        }
    }

    LoadData() {

        super.LoadData();
    }

    AddPages() {
        if (this.navigationGroupId) {
            let req: ComponentRequest<PageSelectBase> = {
                DefaultComponent: "NavigationGroups/NavigationPageCreate",
                Identifier: "CreateModal",
                Init: comp => {
                    comp.pageType = PageTypes.AnyRoutable;
                    comp.Args.Comparer = (d, s) => d.id == s.pageId;
                    comp.Args.CreateNew = d => {
                        return {
                            pageId: d.id,
                            navigationGroupId: this.navigationGroupId,
                            name: d.name,
                            url: d.viewPath
                        }
                    }

                    comp.OnOk = d => {
                        comp.Show = false;
                        return Promise.resolve(true)
                    }
                }
            }
            this.GetComponent(req).then(comp => {
                comp.StartAsync().then(d => {
                    comp.Items = this.list;
                    comp.pageType = PageTypes.UnParameterizedRoutable;
                    if (this.tenantId) {
                        if (comp.tenantId != this.tenantId) {
                            comp.tenantId = this.tenantId;
                            comp.Source.LoadData();
                        } else {
                            comp.Source.Retag();
                        }
                        comp.Show = true;
                    }
                    
                    
                    
                })
            }).catch(error => console.log(error));;
        }
        else
            this.Notify("must_select_nav_group", NoteType.Error, undefined);
    }

    TenantChanged(id: number) {

        this.tenantId = id;

        this.LoadData();
    }

    delete(id: number) {
        Shell.Main.Confirm("delete_confirm_message", true, "Delete").then(() => {
            this.Service.DeleteNavPage(id).then(res => {
                this.LoadData();
            })
        }).catch(() => { })
        
    }

    ApplyOrder(s: number, t: number) {
        this.applyOrder.SourceId = s;
        this.applyOrder.TargetId = t;
        this.Service.SetApplyOrder(this.applyOrder).then(e => this.LoadData());
    }

}