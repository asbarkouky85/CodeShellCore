import { Component, Injector } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { GenerationInofBase } from "@base/generations/generation-inof-base.component";
import { TenantsService } from "@base/http";
import { OutPutListener } from "@base/http/out-put-listener";
import { ServerGenerationService } from "@base/http/server-generation.service";
import { Utils } from "codeshell";
import { ListComponentBase } from "codeshell/base-components";
import { ComponentRequest } from "codeshell/components";

@Component({ template: '' })
export abstract class TenantListBase extends ListComponentBase {
    Service = new TenantsService;
    Gen = new ServerGenerationService();
    Listner = new OutPutListener(true);
    current: string | null | undefined = null;
    modal?: GenerationInofBase;
    url?: string;

    constructor(rt: ActivatedRoute, res: Injector) {
        super(rt, res);
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