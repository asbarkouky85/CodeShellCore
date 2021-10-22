import { Component, ViewChild, Injector } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { GenerationInofBase } from "@base/generations/generation-inof-base.component";
import { DomainsService } from "@base/http";
import { OutPutListener } from "@base/http/out-put-listener";
import { Shell } from "codeshell";
import { EditComponentBase } from "codeshell/base-components";

@Component({ template: '' })
export abstract class ModulesConfigBase extends EditComponentBase {
    Service = new DomainsService();

    @ViewChild("server_tracer")
    server_tracer?: GenerationInofBase;

    Listener: OutPutListener = new OutPutListener(true);

    ModalWidth = "680px";
    OnInstalled?: () => void;

    constructor(rt: ActivatedRoute, inj: Injector) {
        super(rt, inj);
        this.Listener.SendMessage.subscribe((d: any) => {
            if (this.server_tracer)
                this.server_tracer.AddMessage(d);
        });
    }

    UpdateFiles() {
        if (this.server_tracer)
            this.server_tracer.Clear();
        this.Listener.Start().then(d => {
            this.Service.connectionId = d;
            this.Service.UpdateFiles(this.model.assemblyName);
        })
    }

    Install() {
        if (this.server_tracer)
            this.server_tracer.Clear();
        this.Listener.Start().then(d => {
            this.Service.connectionId = d;
            this.Service.InstallModule(this.model.assemblyName).then(d => {
                if (this.OnInstalled)
                    this.OnInstalled();
            });
        })
    }
}