import { Component, ViewChild, Injector } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { DomainTreeBase, DomainCountMode } from "@base/domains/domain-tree-base.component";
import { PagesService, TenantsService, DomainsService } from "@base/http";
import { Tenant } from "@base/models";
import { TenantComponentBase } from "@base/tenant-component-base";
import { Shell } from "codeshell";
import { RecursionModel } from "codeshell/recursion";
import { Stored } from "codeshell/services";
import { PageListBase } from "./page-list-base.component";

export class PTLUseState {
    domainId?: number;
    tenantCode?: string;
}

@Component({ template: '' })
export abstract class PageTreeListBase extends TenantComponentBase {
    Service = new PagesService();
    TenantsService = new TenantsService();
    DomainSrv = new DomainsService();

    @ViewChild("DomainTree") DomainTree?: DomainTreeBase;
    @ViewChild("PageList") PageList?: PageListBase;

    UseState: PTLUseState = new PTLUseState();
    currentDomain?: RecursionModel | null = null;

    get model() { return this.App.UseState; }

    constructor(rt: ActivatedRoute, inj: Injector) {
        super(rt, inj);
    }

    OnReady() {
        Shell.Main.SideBarStatus.emit(false);

        var stor = Stored.Get("PTLUseState", PTLUseState);
        if (stor) {
            this.UseState = stor;
        }

        if (this.PageList) {
            this.PageList.HideHeader = true;
        }


    }

    ngAfterViewInit() {
        super.ngAfterViewInit();
        
        if (this.DomainTree) {

            this.DomainTree.tenantId = this.getTenantId();
            this.DomainTree.UseTenantTree = true;
            this.DomainTree.AllowEdit = true;
            this.DomainTree.HideHeader = true;
            this.DomainTree.OnSelectedEvent = domain => this.OnDomainSelected(domain);
            this.DomainTree.CountMode = DomainCountMode.Pages;

            this.DomainTree.OnTreeLoaded = () => {
                if (this.UseState && this.UseState.tenantCode == this.App.UseState.tenantCode && this.UseState.domainId) {
                    this.DomainTree!.SetSelected(this.UseState.domainId);
                }
            };
            this.DomainTree.LoadData();
        }
    }

    tenantChanged(ten: Tenant | undefined) {

        if (this.DomainTree) {

            this.DomainTree.tenantId = this.getTenantId();
            this.DomainTree.LoadCounts();
            this.LoadPages();
        }
        this.SaveState();
    }

    LoadPages() {
        if (this.PageList) {
            this.PageList.Domain = this.currentDomain;
            this.PageList.tenantId = this.getTenantId();
            this.PageList.LoadData();
        }

    }

    OnDomainSelected(item: RecursionModel | null) {
        this.currentDomain = item;
        if (this.PageList && this.currentDomain) {
            this.LoadPages();
        }
        this.SaveState();

    }

    SaveState() {

        this.UseState.domainId = this.currentDomain ? this.currentDomain.id : undefined;
        this.UseState.tenantCode = this.App.UseState.tenantCode;
        Stored.Set("PTLUseState", this.UseState);
    }

    loadPageTreeList() {

    }


}