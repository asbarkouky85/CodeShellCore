import { Component } from "@angular/core";
import { Injectable } from "@angular/core";
import { NavigationGroupsService } from "@base/http";
import { PageSelectBase, PageTypes } from "@base/pages/page-select-base.component";
import { Shell } from "codeshell";
import { ListComponentBase } from "codeshell/base-components";
import { ComponentRequest } from "codeshell/components";
import { LoadOptions, ListItem } from "codeshell/data";
import { LoadResult, NoteType } from "codeshell/results";

@Component({template:''})
export abstract class NavigationPageListBase extends ListComponentBase {
    get Service(): NavigationGroupsService { return Shell.Injector.get(NavigationGroupsService); }

    tenantId?: number;
    navigationGroupId?: number;
    options: LoadOptions = { Showing: 0, Skip: 0 }
    applyOrder: any = {};

    async LoadDataPromise(): Promise<LoadResult> {
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
            let req: ComponentRequest<PageSelectBase> = { Identifier: "select_pages_modal" }
            this.GetComponent(req).then(comp => {
                comp.pageType = PageTypes.AnyRoutable;
                comp.Items = this.list;
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
                
                if (comp.Source.LoadedOnce){
                    comp.Source.LoadedOnce = comp.tenantId == this.tenantId;
                    comp.Source.Retag();
                }

                comp.pageType = PageTypes.UnParameterizedRoutable;
                if(this.tenantId){
                    comp.tenantId = this.tenantId;
                    comp.StartAsync().then(d => {
                        
                        if (this.tenantId) {
                            comp.Show = true;
                        }
                    })
                }else{
                    this.NotifyTranslate("must_select_tenant", NoteType.Error);
                }
                
            }).catch(error => console.log(error));;
        }
        else
            this.NotifyTranslate("must_select_nav_group", NoteType.Error, undefined);
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