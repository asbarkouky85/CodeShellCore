import { Component, ViewChild, Injector } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { GenerationInofBase } from "@base/generations/generation-inof-base.component";
import { TenantsService } from "@base/http";
import { ConfigPagesService } from "@base/http/config-pages.service";
import { OutPutListener } from "@base/http/out-put-listener";
import { ServerGenerationService } from "@base/http/server-generation.service";
import { SqlCommandsService } from "@base/http/sql-commands.service";
import { PageCategoryCreateBase } from "@base/page-categories/page-category-create-base.component";
import { ServerConfig } from "@base/server-config";
import { Shell, Utils } from "codeshell";
import { DTOEditComponentBase } from "codeshell/base-components";
import { ComponentRequest } from "codeshell/components";
import { FilesHttpService } from "codeshell/http";

@Component({ template: '' })
export abstract class TenantCreateBase extends DTOEditComponentBase {
    Service = new TenantsService();
    Sql = new SqlCommandsService();
    Gen = new ServerGenerationService();
    Pag = new ConfigPagesService();
    Listener = new OutPutListener(true);

    @ViewChild("OutputListener")
    OutputListener?: GenerationInofBase;

    LoadLookups = true;
    environments: any[] = [];
    databases: any[] = [];
    f_logo?: string;
    fService: FilesHttpService = new FilesHttpService("");

    async LoadLookupsAsync(opts: any): Promise<any> {
        this.environments = await this.Sql.GetEnvironments();
        return {};
    }

    constructor(rt: ActivatedRoute, res: Injector) {
        super(rt, res);
        this.Listener.SendMessage.subscribe((d: any) => {

            if (this.OutputListener)
                this.OutputListener.AddMessage(d);
        });
        this.fService = new FilesHttpService(ServerConfig.CurrentApp.configUrl);
    }

    DefaultModel() {
        return {
            entity: {
                mainComponentBase: "AppComponent"
            }
        }
    }

    OnReady() {
        if (this.OutputListener) {
            this.OutputListener.HideHeader = true;
        }

    }

    env_change() {
        this.databases = [];
        if (this.model.environment) {
            var env = this.environments.find(d => d.name == this.model.environment);
            if (env) {
                for (var d of env.databases) {
                    this.databases.push({ name: d });
                }
            }
        }
    }

    upload = (f: FileList) => {
        return this.fService.PostFiles("Upload", f);
    }

    SelectTemplate() {
        var req: ComponentRequest<PageCategoryCreateBase> = {
            Identifier: "SelectTemplateModel"
        }
        this.GetComponent(req).then(comp => {
            comp.StartAsync().then(d => {
                comp.Show = true;
                comp.OnOk = d => {
                    this.model.entity.mainComponentBase = comp.SelectedItems[0].viewPath;
                    return Promise.resolve(true);
                }
            })
        });
    }

    SyncTenant() {
        if (this.OutputListener)
            this.OutputListener.Clear();
        this.Listener.Start().then(conn => {
            this.Gen.connectionId = conn;
            let parent = this.model.entity.parentId ? this.model.entity.parentId : 1;
            this.Gen.SyncTenants(parent, this.model.entity.id);
        })
    }

    UpdateDatabase() {
        if (this.OutputListener)
            this.OutputListener.Clear();

        this.Listener.Start().then(conn => {
            this.Sql.connectionId = conn;

            this.Sql.UpdateTenantDatabse({
                dbName: this.model.dbName,
                environment: this.model.environment,
                id: this.model.entity.id
            })
        })
    }

    Render() {
        if (this.OutputListener)
            this.OutputListener.Clear();
        this.Listener.Start().then(conn => {
            this.Gen.connectionId = conn;

            this.Gen.RenderTenant({ mod: this.model.entity.code })
        })
    }

    Publish() {
        if (this.OutputListener)
            this.OutputListener.Clear();


        this.Listener.Start().then(conn => {
            this.Gen.connectionId = conn;

            this.Gen.PublishTenant({
                tenantCode: this.model.entity.code,
                environment: this.model.environment,
                force: this.model.force
            }).then(d => {

                if (d.message != "already_rendered") {
                    Shell.Main.ShowPromptTranslate(d.message);
                }
            }).catch(d => {
                Utils.HandleError(d, true);
            });
        })
    }

    Submit() {
        if (this.IsNew && this.model.dbName) {
            if (this.OutputListener)
                this.OutputListener.Clear();
            this.Listener.Start().then(d => {
                this.Sql.connectionId = d;
                var req = {
                    tenantCode: this.model.entity.code,
                    environment: this.model.environment,
                    dbName: this.model.dbName,
                    id: 0
                };
                this.Sql.CreateTenantDatabase(req).then(d => {
                    if (!this.model.entity.connectionString)
                        this.model.entity.connectionString = d.data.ConnectionString;
                    this.model.entity.id = d.data.TenantId;
                    this.NotifyTranslate("database_created");

                    this.Service.Post("Post", this.model.entity).then(d => {
                        super.OnSubmitSuccess(d);
                    });
                }).catch(d => Utils.HandleError(d, true));
            })
        } else {
            super.Submit();
        }


    }
    ngOnDestroy() {
        super.ngOnDestroy();
        this.Listener.Stop();
    }
}