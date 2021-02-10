import { ListComponentBase, EditComponentBase, BaseComponent } from "codeshell/baseComponents";
import { Injectable } from "@angular/core";
import { Shell } from "codeshell/core";
import { PageControlsService } from "../Http/PageControlsService";
import { ListItem, Utils, LoadOptions, ComponentRequest, NoteType, SubmitResult } from "../../codeshell/helpers";
import { Router } from "@angular/router";
import { PageParameterDTO } from "Example/Dtos";
import { PagesService } from "Example/Http/PagesService";
import { PageParameterTypes } from "Example/Enumerations";
import { PageSelectBase, PageTypes } from "Example/Pages/PageSelectBase";
import { ServerConfig } from "Example/ServerConfig";
import { ServerGenerationService } from "Example/Http/ServerGenerationService";
import { Permission } from "codeshell/security";

@Injectable()
export abstract class PageControlListBase extends BaseComponent {
    get Service(): PageControlsService { return Shell.Injector.get(PageControlsService); }
    get Pages(): PagesService { return Shell.Injector.get(PagesService); }

    Gen: ServerGenerationService = new ServerGenerationService();

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
                this.tenantId = d.tenantId;
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
            presistant: this.page.presistant
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
        console.log(this.pageRoute);
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

        var req: ComponentRequest<PageSelectBase> = {
            Identifier: "PageSelector",
            Init: comp => {
                comp.tenantId = this.tenantId;
                comp.pageType = type;
                comp.Args.CreateNew = d => {
                    return { id: d.id, viewPath: d.viewPath }
                }
                comp.Multi = false;
            }
        }
        return new Promise((res, rej) => {

            this.GetComponent(req).then(comp => {
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