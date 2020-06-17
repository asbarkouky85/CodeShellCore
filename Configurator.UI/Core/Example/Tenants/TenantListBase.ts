import { ListComponentBase } from "codeshell/baseComponents";
import { Injectable } from "@angular/core";
import { TenantsService } from "Example/Http";
import { Shell } from "codeshell/core";
import { ComponentRequest, Utils } from "../../codeshell/helpers";
import { TenantCreateBase } from "./TenantCreateBase";
import { OutPutListener } from "../Http/OutPutListener";
import { ServerGenerationService } from "Example/Http/ServerGenerationService";
import { GenerationInofBase } from "Example/Generations/GenerationInofBase";
import { ActivatedRoute } from "@angular/router";

@Injectable()
export abstract class TenantListBase extends ListComponentBase{
    get Service(): TenantsService { return Shell.Injector.get(TenantsService); }
    Gen: ServerGenerationService = new ServerGenerationService();
    Listner = new OutPutListener(true);
    current: string | null | undefined = null;
    modal?: GenerationInofBase;
    url?: string;

    constructor(rt: ActivatedRoute) {
        super(rt);
        this.Listner.SendMessage.subscribe((d: any) => {
            if (this.modal)
                this.modal.AddMessage(d);
        })
    }

    ngOnInit() {
        super.ngOnInit();
        this.Gen.GetActivePreview().then(d => {
            this.current = d.tenantCode;
            this.url = d.url as string;
        });
        
    }

    async GetNotifier(): Promise<GenerationInofBase> {
        var req: ComponentRequest<GenerationInofBase> = { Identifier: "ServerTrace" }
        this.modal = await this.GetComponent(req);
        return this.modal;
    }

    StartPreview(model: any) {
        this.GetNotifier().then(d => {
            this.modal = d;
            d.Clear();
            d.Show = true;
            this.Listner.Start().then(d => {
                this.Gen.connectionId = d;
                this.Gen.StartTenantPreview({ tenantCode: model.code }).then(d => {
                    this.url = "";
                    if (d.data.Url) {
                        this.url = d.data.Url;
                        this.Open();
                    }
                        
                    this.current = model.code;

                }).catch(er => {
                    Utils.HandleError(er, true);
                })
            })

        })
    }

    StopPreview() {
        this.GetNotifier().then(comp => {
            comp.Show = true;
            comp.Clear();
            this.Listner.Start().then(res => {
                this.Gen.connectionId = res;
                this.Gen.StopPreview().then(d => this.current = null);
            })
        })
        
    }

    Open() {
        if (this.url)
            window.open(this.url);
    }

    isActive_change(model: any) {
        this.Service.Update("Put", model);
    }
}