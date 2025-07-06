import { Injectable, ViewChild, ElementRef, Injector, Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { OutPutListener } from "@base/http/out-put-listener";
import { ServerGenerationService } from "@base/http/server-generation.service";
import { LogMessage } from "@base/models";
import { BaseComponent } from "codeshell/base-components";
import { SubmitResult } from "codeshell/results";


@Component({ template: '' })
export abstract class GenerationInofBase extends BaseComponent {

    @ViewChild("container") _containerRef?: ElementRef;

    Container?: HTMLElement;

    list: any[] = [];
    renderModel: any = {};

    OutListener = new OutPutListener();
    Generation = new ServerGenerationService();
    CurrentLang = "ar";

    constructor(rt: ActivatedRoute, res: Injector) {
        super(rt, res);
        this.OutListener.SendMessage.subscribe(e => {
            this.AddMessage(e);
        });
    }

    ngOnInit() {

    }

    ngAfterViewInit() {
        if (this._containerRef)
            this.Container = this._containerRef.nativeElement;
    }

    Clear() {
        this.list = [];
    }

    AddMessage(d: LogMessage) {
        this.list.push(d);
        if (d.isNew)
            this.list.push({ newLine: true });
        if (this.Container) {
            this.Container.scroll(0, this.Container.scrollHeight);
        }
    }

    async RenderAll(code: string): Promise<SubmitResult> {
        this.Clear();
        this.Show = true;
        var conn = await this.OutListener.Start();
        this.Generation.connectionId = conn;
        return await this.Generation.RenderTenant({ mod: code });
    }

    async Definition(code: string): Promise<SubmitResult> {
        this.Clear();
        this.Show = true;
        var conn = await this.OutListener.Start();
        this.Generation.connectionId = conn;
        return await this.Generation.ModuleDefinition({ mod: code });
    }


}