import { Component } from "@angular/core";
import { PageParameterDTO } from "@base/dtos";
import { PageParameterTypes } from "@base/enumerations";
import { PageControlsService, PagesService } from "@base/http";
import { ServerGenerationService } from "@base/http/server-generation.service";
import { PageTypes, PageSelectBase } from "@base/pages/page-select-base.component";
import { Shell, Utils } from "codeshell";
import { BaseComponent, EditComponentBase } from "codeshell/base-components";
import { ComponentRequest } from "codeshell/components";
import { ListItem, LoadOptions } from "codeshell/data";
import { SubmitResult } from "codeshell/results";
import { Permission } from "codeshell/security";

@Component({ template: '' })
export abstract class PageControlListBase extends BaseComponent {
    Service = new PageControlsService()
    Pages = new PagesService();

    Gen: ServerGenerationService = new ServerGenerationService();


    CurrentLang = "en";
    list: any[] = [];
    pageId?: number;
    tenantId: number = 0;
    pageRoute: any = {};
    ActiveTab: string = "Controls";
    Parameters: ListItem[] = [];
    Fields: ListItem[] = [];
    page: any = {};
    newField: any = {};
    subPermission = Permission.FullAccess;

    options: LoadOptions = { Showing: 0, Skip: 0 }
    accessabilityList: any[] = [
        { id: 0, name: "Hide" },
        { id: 1, name: "Read Only" },
        { id: 2, name: "Read Write" }
    ];

    ngOnInit() {
        super.ngOnInit();
        this.Route.params.subscribe(par => {

            if (par["id"]) {
                this.pageId = parseInt(par["id"]);

                this.LoadLookups();
                this.LoadData()
            }

        })

    }
    OnReady() {
        Shell.Main.SideBarStatus.emit(true);

    }

    LoadLookups() {
        var opts = this.GetLookupOptions();

        this.Service.Get("GetEditLookups", opts).then(lookups => {
            this.Lookups = lookups;
            this.Lookups.Accessability = this.accessabilityList;
        })
    }

    LoadData() {
        if (this.pageId) {
            this.Pages.GetCustomizationData(this.pageId).then(d => {
                this.list = ListItem.Convert(d.controls);
                this.Parameters = ListItem.Convert(d.parameters);
                this.Fields = ListItem.Convert(d.fields);
                this.pageRoute = d.route;
                this.page.display = d.display;
                this.page.layout = d.layout;
                this.tenantId = d.tenantId;
                Shell.Main.SetTitle(this.page.display, false);
            });
        }
    }

    ProcessTemplate() {
        if (this.pageId) {
            this.Gen.ProcessForPage(this.pageId).then(d => {
                this.LoadData();
            }).catch(d => Utils.HandleError(d, true));
        }
    }

    Render() {

        this.SaveChangesAsync().then(res => {
            if (this.pageId) {
                this.Gen.RenderPage(this.pageId).then(d => {
                    this.NotifyTranslate(d.message);
                    this.LoadData();
                }).catch(d => Utils.HandleError(d, true));
            }
        })
    }

    async SaveChangesAsync(): Promise<SubmitResult> {
        var data = {
            controls: ListItem.GetChangedItems(this.list),
            parameters: ListItem.GetChangedItems(this.Parameters),
            fields: ListItem.GetChangedItems(this.Fields),
            route: this.pageRoute.isChanged ? this.pageRoute : null,
            id: this.pageId,
            presistant: this.page.presistant,
            layout: this.page.layout
        }

        return await this.Pages.ApplyCustomization(data);
    }

    update() {

        this.SaveChangesAsync().then(res => {
            this.LoadData();
            this.Notify("success_message");
        }).catch(err => {
            Utils.HandleError(err, true);
        })
    }

    Clear(type: string) {

        this.pageRoute[type + "String"] = null;
        this.pageRoute[type] = null;
        this.pageRoute.isChanged = true;
    }

    ClearValue(par: any) {
        par.viewPath = null;
        par.entity.linkedPageId = null;
        par.SetModified();
    }

    SelectComponent(type: string | null, parType: any = null) {
        var prom: Promise<{ id: number, viewPath: string }> | null = null;

        if (type) {
            switch (type) {
                case "listUrl":
                case "addUrl":
                    prom = this.OpenPageSelector(PageTypes.UnParameterizedRoutable);
                    break;
                case "editUrl":
                case "detailsUrl":
                    prom = this.OpenPageSelector(PageTypes.ParameterizedRoutable);
                    break;

            }

            if (prom) {
                prom.then(d => {
                    this.pageRoute[type + "String"] = d.viewPath;
                    this.pageRoute[type] = d.id;

                    this.pageRoute.isChanged = true;
                });
            }
        } else if (parType) {
            switch (parType.type) {
                case PageParameterTypes.Embedded:
                case PageParameterTypes.Modal:
                    prom = this.OpenPageSelector(PageTypes.Embedded);
                    break;
                case PageParameterTypes.PageLink:
                    prom = this.OpenPageSelector(PageTypes.AnyRoutable);
                    break;
            }
            if (prom) {
                prom.then(d => {
                    parType.viewPath = d.viewPath;
                    parType.entity.linkedPageId = d.id;
                    parType.entity.useDefault = false;
                    (parType as ListItem).SetModified();
                })
            }
        }


    }

    OpenPageSelector(type: PageTypes): Promise<{ id: number, viewPath: string }> {
        var x = (n: any) => { return n };

        var req: ComponentRequest<PageSelectBase> = { Identifier: "PageSelector" }
        return new Promise((res, rej) => {

            this.GetComponent(req).then(comp => {
                comp.tenantId = this.tenantId;
                comp.pageType = type;
                comp.Args.CreateNew = d => {
                    return { id: d.id, viewPath: d.viewPath }
                }
                comp.Multi = false;
                comp.OnOk = d => {
                    if (comp.Items[0])
                        res(comp.Items[0] as any);
                    return Promise.resolve(true);
                }
                if (comp.pageType != type) {
                    comp.pageType = type;
                    comp.Source.LoadedOnce = false;
                }
                comp.StartAsync().then(d => {
                    comp.Show = true;
                })
            })
        });



    }

    AddNewField() {
        var n = ListItem.Detached(this.newField);
        (n as any).pageId = this.pageId;
        n.AddTo(this.Fields);
        this.newField = {};
    }

    IsPageReference(par: PageParameterDTO): boolean { return par.type != PageParameterTypes.Text; }
}