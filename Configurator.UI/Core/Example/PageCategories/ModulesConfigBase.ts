import { EditComponentBase } from "codeshell/baseComponents";
import { Injectable, ViewChild } from "@angular/core";
import { DomainsService } from "Example/Http";
import { Shell } from "codeshell/core";
import { GenerationInofBase } from "../Generations/GenerationInofBase";
import { OutPutListener } from "../Http/OutPutListener";
import { ActivatedRoute } from "@angular/router";

@Injectable()
export abstract class ModulesConfigBase extends EditComponentBase {
    get Service(): DomainsService { return Shell.Injector.get(DomainsService); }

    @ViewChild("server_tracer")
    server_tracer?: GenerationInofBase;

    Listener: OutPutListener = new OutPutListener(true);

    ModalWidth = "680px";
    OnInstalled?: () => void;

    constructor(rt: ActivatedRoute) {
        super(rt);
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