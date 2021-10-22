import { Injectable, ViewChild, Injector, Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { NavigationGroupsService } from "@base/http";
import { Tenant } from "@base/models";
import { TenantComponentBase } from "@base/tenant-component-base";
import { Shell } from "codeshell";
import { ListItem } from "codeshell/data";
import { NoteType } from "codeshell/results";
import { NaveListBase } from "./nave-list-base.component";
import { NavigationPageListBase } from "./navigation-page-list-base.component";

@Component({template:''})
export abstract class NavGroupPagesBase extends TenantComponentBase {
    get Service(): NavigationGroupsService { return Shell.Injector.get(NavigationGroupsService); }

    @ViewChild("NaveList") NaveList?: NaveListBase;
    @ViewChild("NavigationPageList") NavigationPageList?: NavigationPageListBase;

    constructor(rt: ActivatedRoute, inj: Injector) {
        super(rt, inj);
    }

    OnReady() {
        Shell.Main.SideBarStatus.emit(false);
        if (this.App.UseState.tenantCode) {
            this.loadNaveGroupPages();
        }
    }

    getPages(item: any) {
        if (this.NavigationPageList) {
            this.Service.naveId = item;
            this.NavigationPageList.tenantId = this.getTenantId();
            this.NavigationPageList.navigationGroupId = item;
            this.NavigationPageList.LoadData();
        }
    }

    AddPage() {
        if (this.NavigationPageList) {
            this.NavigationPageList.AddPages();
        }

    }

    tenantChanged(ten: Tenant | undefined) {
        if (this.NavigationPageList) {
            var id = this.getTenantId();
            if (id)
                this.NavigationPageList.TenantChanged(id);
        }
        this.loadNaveGroupPages();
    }

    loadNaveGroupPages() {
        if (this.NaveList) {
            this.NaveList.LoadData();
        }
    }

    save() {

        if (this.NavigationPageList) {
            var changed = ListItem.GetChangedItems(this.NavigationPageList.list);
            this.Service.Post("Create", changed).then(res => {

                this.Notify("Changed Successfully", NoteType.Success);
                if (this.NavigationPageList) {
                    this.NavigationPageList.LoadData();
                }
            })

        }

    }
}