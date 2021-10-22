import { Injectable, ViewChild, Injector, Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { DomainTreeBase, DomainCountMode } from "@base/domains/domain-tree-base.component";
import { DbCreationRequest } from "@base/dtos";
import { BuilderService } from "@base/http/builder.service";
import { OutPutListener } from "@base/http/out-put-listener";
import { ServerGenerationService } from "@base/http/server-generation.service";
import { SqlCommandsService } from "@base/http/sql-commands.service";
import { TenantComponentBase } from "@base/tenant-component-base";
import { Shell } from "codeshell";
import { RecursionModel } from "codeshell/recursion";
import { NoteType } from "codeshell/results";
import { GenerationInofBase } from "./generation-inof-base.component";

@Component({template:''})
export abstract class GenerationNotificationBase extends TenantComponentBase {
    ServerGen: ServerGenerationService = new ServerGenerationService();

    @ViewChild("DomainTree") DomainTree?: DomainTreeBase;
    @ViewChild("GenerationInofList") GenerationInofList?: GenerationInofBase;


    get TenantCode() { return this.App.UseState.tenantCode; }
    model = new DbCreationRequest();

    ServerListener = new OutPutListener(true);
    Sql = new SqlCommandsService();
    Builder = new BuilderService();
    ActiveTab = "domains_tab";
    renderModel: any = {};

    tree = new RecursionModel();
    environments: any[] = [];

    constructor(rt: ActivatedRoute, res: Injector) {
        super(rt, res);
        this.model.recursive = true;
        this.ServerListener.SendMessage.subscribe((d: any) => {
            if (this.GenerationInofList)
                this.GenerationInofList.AddMessage(d);
        });
    }


    OnReady() {
        this.ServerGen.Silent = true;

        if (this.DomainTree) {

            this.DomainTree.tenantId = this.getTenantId();

            this.DomainTree.AllowEdit = false;
            this.DomainTree.HideHeader = true;
            this.DomainTree.isGeneration = true;
            this.DomainTree.CountMode = DomainCountMode.All;
            this.DomainTree.RenderRequest.subscribe((d: RecursionModel) => this.render("domain", d));
            this.DomainTree.ProcessRequest.subscribe((d: RecursionModel) => this.process(d));
            this.DomainTree.LoadData();
        }

        Shell.Main.SideBarStatus.emit(false);

        this.Sql.GetEnvironments().then(d => {
            this.environments = d;
            this.environments.push({ id: 0, name: "(Current Machine)" })
        });
    }

    tenantChanged(ten: any) {
        if (this.DomainTree) {
            this.DomainTree.tenantId = this.getTenantId();
            this.DomainTree.LoadCounts();
        }
    }

    ClearTracer() {
        if (this.GenerationInofList)
            this.GenerationInofList.Clear();
    }

    Mapping() {
        this.ClearTracer();
        this.ServerListener.Start().then(d => {
            this.ServerGen.connectionId = d;
            this.ServerGen.Mapping();
        })
    }

    InitializeApp() {
        this.ClearTracer();
        this.ServerListener.Start().then(d => {
            this.Builder.connectionId = d;
            this.Builder.Init(this.model.replaceExisting);
        })
    }

    AddStaticFiles() {
        this.ClearTracer();
        this.ServerListener.Start().then(d => {
            this.Builder.connectionId = d;
            this.Builder.AddStaticFiles(this.model.replaceExisting);
        })
    }

    AddBaseScripts() {
        this.ClearTracer();
        this.ServerListener.Start().then(d => {
            this.Builder.connectionId = d;
            this.Builder.AddBaseScripts(this.model.replaceExisting);
        })
    }

    AddShellComponents() {
        this.ClearTracer();
        this.ServerListener.Start().then(d => {
            this.Builder.connectionId = d;
            this.Builder.AddShellComponents(this.model.replaceExisting);
        })
    }

    WriteWebPackFiles() {
        this.ClearTracer();
        this.ServerListener.Start().then(d => {
            this.Builder.connectionId = d;
            this.Builder.WriteWebPackFiles();
        })
    }

    PrepEnvironment() {
        this.ClearTracer();
        var s = confirm("Bundle production vendor");
        this.ServerListener.Start().then(d => {
            this.Builder.connectionId = d;
            this.Builder.PrepEnvironment(s);
        })
    }

    ClearOlderBundles() {
        this.ClearTracer();
        this.ServerListener.Start().then(d => {
            this.Builder.connectionId = d;
            this.Builder.ClearOlderBundles({ environment: this.model.environment, tenantCode: this.TenantCode, recursive: true, replaceExisting: false });
        })
    }


    InitializeResx() {
        this.ClearTracer();
        this.ServerListener.Start().then(d => {
            this.Builder.connectionId = d;
            this.Builder.InitializeResx();
        })
    }


    SyncLanguages() {
        this.ClearTracer();
        this.ServerListener.Start().then(d => {
            this.Builder.connectionId = d;
            this.Builder.SyncLanguages();
        })
    }

    FixPages() {
        this.ClearTracer();
        this.ServerListener.Start().then(d => {
            this.Builder.connectionId = d;
            this.Builder.FixPages();
        })
    }

    process(domain: any) {
        if (this.GenerationInofList) {
            this.GenerationInofList.list = [];
        }
        this.ServerListener.Start().then(d => {
            this.ServerGen.connectionId = d;
            this.ServerGen
                .Process({ nameChain: domain.nameChain, mod: this.App.UseState.tenantCode })
        });
    }

    render(type: string, item: RecursionModel | null = null) {
        document.body.scroll(0, document.body.scrollHeight);
        var code = this.App.UseState.tenantCode;
        if (code) {

            if (this.GenerationInofList) {
                this.GenerationInofList.list = [];
            }


            this.ServerListener.Start().then(d => {
                this.ServerGen.connectionId = d;
                switch (type) {
                    case "all":
                        this.ServerGen
                            .RenderTenant({ mod: code, recursive: true })

                        break;
                    case "definition":
                        this.ServerGen
                            .ModuleDefinition({ mod: code })

                        break;
                    case "domain":
                        if (item) {
                            this.ServerGen
                                .Render({ mod: code, nameChain: item.nameChain, recursive: this.model.recursive })

                        }

                        break;
                }
            })

        }
        else {
            this.NotifyTranslate("must_select_tenant", NoteType.Error, undefined);
        }
    }


}