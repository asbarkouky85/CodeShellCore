import { EditComponentBase } from "codeshell/baseComponents";
import { Injectable } from "@angular/core";
import { PagesService } from "../Http/PagesService";
import { Shell } from "../../codeshell/core";
import { ComponentRequest, RecursionModel, NoteType, SubmitResult, Stored } from "../../codeshell/helpers";
import { DomainTreeBase } from "../Domains/DomainTreeBase";
import { Router } from "@angular/router";
import { AppComponentBase } from "Example/AppComponentBase";
import { ServerGenerationService } from "Example/Http/ServerGenerationService";
import { ConfigPagesService } from "Example/Http/ConfigPagesService";
import { PTLUseState } from "./PageTreeListBase";
import { DomainsService } from "Example/Http/DomainsService";

@Injectable()
export abstract class PageCreateBase extends EditComponentBase {
    get Service(): PagesService { return Shell.Injector.get(PagesService); }
    Pages: ConfigPagesService = new ConfigPagesService();
    DomainSrv: DomainsService = new DomainsService();

    action: string = "";
    get App(): AppComponentBase { return Shell.Main as AppComponentBase; }

    OnReady() {
        Shell.Main.SideBarStatus.emit(true);
        var stat = Stored.Get("PTLUseState", PTLUseState);
        if (stat && stat.domainId) {
            this.DomainSrv.Get("GetSingle/" + stat.domainId).then(d => {
                this.model.componentDomain = d.nameChain;
                this.writePath();

            })
        }
        if (this.App.UseState.tenantCode) {
            this.model.tenantCode = this.App.UseState.tenantCode;
        }
    }

    writePath() {
        this.model.componentPath = this.fixComponentDomain();

        if (this.model.componentName)
            this.model.componentPath += this.model.componentName;
    }

    fixComponentDomain(): string {
        var res = this.model.componentDomain;
        if (res && res.length > 0) {
            if (res[0] == "/")
                res = res.substr(1);
            if (res[res.length - 1] != "/")
                res += "/";
        } else {
            res = "";
        }
        return res;
    }

    selectDomain() {
        let req: ComponentRequest<DomainTreeBase> = {
            DefaultComponent: "Pages/DomainTreeSelect",
            Identifier: "DomainModal",
            Init: comp => {
                comp.LoadData();
                comp.AllowEdit = false;
                comp.OnOk = () => {
                    comp.Show = false;
                    var item = comp.selectedItem as RecursionModel
                    this.model.componentDomain = item.nameChain;
                    this.writePath();
                    return Promise.resolve(true);
                }
            }
        }
        this.GetComponent(req).then(comp => {
            comp.StartAsync().then(d => { d.Show = true; });
        }).catch(error => console.log(error));;
    }

    OnSubmitSuccess(res: SubmitResult) {

        if (this.IsNew && res.data.Id) {
            this.NotifyTranslate(res.message, NoteType.Success);
            var url = this.ViewParams.Other["CustomizeLink"];
            if (url)
                this.NavigateToComponent(url + "/" + res.data.Id);
        }
        else {
            super.OnSubmitSuccess(res);
            if (res.data.MoveRequest) {
                this.Pages.PageMoved(res.data.MoveRequest);
            }
        }

    }

    TextBoxWithButtonClick(prop: string) {
        this.selectDomain();
    }

    DefaultModel() {
        return this.model = {
            usage: 'R',
            actionType: 'view',
            specialPermission: 'anonymous',
            defaultAccessibility: 2
        }
    }
}