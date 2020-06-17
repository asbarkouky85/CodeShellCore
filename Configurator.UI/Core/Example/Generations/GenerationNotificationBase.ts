import { Injectable, ViewChild } from "@angular/core";
import { Shell } from "codeshell/core";
import { DomainTreeBase, DomainCountMode } from "../Domains/DomainTreeBase";
import { RecursionModel, NoteType } from "../../codeshell/helpers";
import { OutPutListener } from "../Http/OutPutListener";
import { GenerationInofBase } from "./GenerationInofBase";
import { TenantComponentBase } from "Example/TenantComponentBase";
import { ServerGenerationService } from "Example/Http/ServerGenerationService";
import { SqlCommandsService } from "Example/Http/SqlCommandsService";
import { BuilderService } from "Example/Http/BuilderService";
import { DbCreationRequest } from "Example/Dtos";
import { ActivatedRoute } from "@angular/router";

@Injectable()
export abstract class GenerationNotificationBase extends TenantComponentBase {
    ServerGen: ServerGenerationService = new ServerGenerationService();

    @ViewChild("DomainTree") DomainTree?: DomainTreeBase;
    @ViewChild("GenerationInofList") GenerationInofList?: GenerationInofBase;

    model = new DbCreationRequest();

    ServerListener = new OutPutListener(true);
    Sql = new SqlCommandsService();
    Builder = new BuilderService();
    ActiveTab = "domains_tab";
    renderModel: any = {};

    tree = new RecursionModel();
    environments: any[] = [];

    constructor(rt: ActivatedRoute) {
        super(rt);
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
            this.DomainTree.CountMode = DomainCountMode.Pages;
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

    ClearTracer() {
        if (this.GenerationInofList)
            this.GenerationInofList.Clear();
    }

    InitializeApp() {
        this.ClearTracer();
        this.ServerListener.Start().then(d => {
            this.Builder.connectionId = d;
            this.Builder.Init();
        })
    }

    AddStaticFiles() {
        this.ClearTracer();
        this.ServerListener.Start().then(d => {
            this.Builder.connectionId = d;
            this.Builder.AddStaticFiles();
        })
    }

    AddBaseScripts() {
        this.ClearTracer();
        this.ServerListener.Start().then(d => {
            this.Builder.connectionId = d;
            this.Builder.AddBaseScripts();
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
            this.Builder.ClearOlderBundles({ environment: this.model.environment, tenantCode: this.model.tenantCode });
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
    
    process(domain: any) {
        if (this.GenerationInofList) {
            this.GenerationInofList.list = [];
        }
        this.ServerListener.Start().then(d => {
            this.ServerGen.connectionId = d;
            this.ServerGen
                .Process({ nameChain: domain.nameChain, mod: this.model.tenantCode })
        });
    }

    render(type: string, item: RecursionModel | null = null) {
        document.body.scroll(0, document.body.scrollHeight);
        if (this.model.tenantCode) {

            if (this.GenerationInofList) {
                this.GenerationInofList.list = [];
            }


            this.ServerListener.Start().then(d => {
                this.ServerGen.connectionId = d;
                switch (type) {
                    case "all":
                        this.ServerGen
                            .RenderTenant({ mod: this.model.tenantCode })
                            
                        break;
                    case "definition":
                        this.ServerGen
                            .ModuleDefinition({ mod: this.model.tenantCode })
                            
                        break;
                    case "domain":
                        if (item) {
                            this.ServerGen
                                .Render({ mod: this.model.tenantCode, nameChain: item.nameChain })
                                
                        }

                        break;
                }
            })

        }
        else {
            this.Notify("must_select_tenant", NoteType.Error, undefined);
        }
    }


}