import { Injectable, OnInit, ViewChild, ElementRef, Input, ComponentRef, OnDestroy, ComponentFactoryResolver, ComponentFactory, Injector } from "@angular/core";
import { ActivatedRoute, Route, Router, NavigationExtras } from "@angular/router";
import { Location } from "@angular/common";

import { Shell } from "../shell";
import { Permission, RouteData } from "../security/models";
import { NoteType, ViewParams, DeleteResult, ComponentRequest, LocalizablesDTO } from "../helpers";
import { Registry } from "codeshell/main";

@Injectable()
export abstract class BaseComponent implements OnInit, OnDestroy {

    UseLocalization: boolean = false;
    get Loc(): LocalizablesDTO { return new LocalizablesDTO() };
    private _subs: { [key: string]: any } = {};
    private _viewParams?: ViewParams;
    private _modalOk = false;
    private _modalCancel = false;

    protected Route: ActivatedRoute;
    protected Injector: Injector;
    protected get Resolver(): ComponentFactoryResolver { return this.Injector.get(ComponentFactoryResolver); };
    protected RouteData: RouteData = new RouteData;

    EmbeddedInit?: () => void;

    onlyNumber(evt: any) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
    get Navigation(): Location { return Shell.Injector.get(Location); }
    Lookups: { [key: string]: any[] } = {};

    Debug: boolean = true;
    Permission: Permission = Permission.Anonymous;
    ScreenHeight = window.innerHeight + 'px';
    Title: string = "";
    HeaderExtra?: string;

    IsInitialized: boolean = false;
    Show: boolean = false;
    ShowOnReady: boolean = false;

    HideHeader: boolean = false;
    IsChild = false;
    ModalWidth: any = 768;


    @Input("IsEmbedded") IsEmbedded: boolean = false;
    @ViewChild("viewParamsContainer") paramsContainer?: ElementRef;
    @ViewChild("lookupOptionsContainer") lookupsContainer?: ElementRef;

    OnOk: (sender: any) => Promise<boolean> = e => Promise.resolve(true);
    OnCancel: (sender: any) => Promise<boolean> = e => Promise.resolve(true);

    get ViewParams(): ViewParams {

        if (!this._viewParams) {
            this._viewParams = this.LoadViewParams();
        }
        return this._viewParams;
    }

    get ComponentName(): string {
        return (this as any).constructor.name;
    }

    get Router(): Router {
        return Shell.Injector.get(Router);
    }

    constructor(route: ActivatedRoute, inj: Injector) {
        this.Route = route;
        this.Injector = inj;
        let conf = route.routeConfig as Route;

        if (conf) {

            this.RouteData = Object.assign(new RouteData, conf.data);
            Shell.Main.SetTitle(this.RouteData.name);

            if (this.RouteData.IsAnonymous) {
                this.Permission = Permission.FullAccess;
            }
            else if (!Shell.Session.IsLoggedIn) {
                Shell.Injector.get(Router).navigate(["/Login"]);
            } else if (this.RouteData.AllowAll) {
                this.Permission = Permission.FullAccess;
            } else {
                this.Permission = Shell.Session.GetPermission(this.RouteData.resource);
            }

        } else {
            this.IsEmbedded = true;
        }


    }

    GetPageId(): number { return 0; }

    GetMainUrl(): string {
        return Shell.Main.GetMainUrl();
    }

    ngAfterViewInit() {
        Shell.ViewLoaded.emit({});
    }

    Log(...params: any[]) {
        if (this.Debug) {
            console.log(this.ComponentName, ...params)
        }
    }

    SortBy(prop: string) { }

    GetHeaderClass(prop: string): string | null { return null; }

    private LoadViewParams(): ViewParams {
        if (this.paramsContainer) {
            var s = this.GetObjectFromHtmlAs(this.paramsContainer, ViewParams);
            if (s != null)
                return s;
        }
        return new ViewParams;
    }

    GetObjectFromHtml(ref: ElementRef): any | null {
        let el = ref.nativeElement as HTMLElement;
        var attr = el.attributes.getNamedItem("values");

        if (attr != null && attr.value.length > 0) {
            var s = eval('(' + attr.value + ')')
            if (s) {
                return s;
            }
        }
        return null;
    }

    GetObjectFromHtmlAs<T>(ref: ElementRef, type: new () => T): T | null {
        var s = this.GetObjectFromHtml(ref);
        if (s != null)
            return Object.assign(new type, s);
        return null;
    }

    OnConstructed() { }

    async GetComponent<T extends BaseComponent>(opener: ComponentRequest<T>, createNew: boolean = false): Promise<T> {

        let comp: ComponentRef<T> | null;

        if (createNew)
            Shell.Main.ClearModalLoader();

        if (this._subs[opener.Identifier] == undefined) {
            comp = await this.LoadComponentFromParams<T>(
                opener.Identifier,
                opener.DefaultComponent
            );

            if (!comp) {
                this._subs[opener.Identifier] = null;
            }
            else if (opener.Init) {
                opener.Init(comp.instance);
            }
            if (!createNew)
                this._subs[opener.Identifier] = comp;
        } else {
            comp = this._subs[opener.Identifier];
        }

        if (comp) {

            this.LoadComponentRef(comp);
            return Promise.resolve(comp.instance);
        }
        else
            return Promise.reject("failed to obtain");

    }

    private OpenModal<T extends BaseComponent>(path: string): Promise<ComponentRef<T>> {
        //var comp = Shell.Main.GetDialogAs<T>(path);
        let e = Registry.Get(path);
        if (e) {
            console.log(this.Resolver)
            var fac = this.Resolver.resolveComponentFactory(e) as ComponentFactory<T>;
            let comp: ComponentRef<T> = fac.create(Shell.Injector);
            if (comp != null && Shell.Main.ModalLoader) {
                Shell.Main.ModalLoader.UseComponent(comp);
                return Promise.resolve(comp);
            }
        }

        return Promise.reject("not found");
    }

    private LoadComponentRef<T extends BaseComponent>(ref: ComponentRef<T>) {

        if (Shell.Main.ModalLoader) {
            Shell.Main.ModalLoader.UseComponent(ref);
        }
    }

    private LoadComponentFromParams<T extends BaseComponent>(fromOther: string, def?: string): Promise<ComponentRef<T>> {

        var path = this.ViewParams.Other[fromOther];
        if (path) {
            return this.OpenModal<T>(path);
        } else if (def) {
            return this.OpenModal<T>(def);
        }

        return Promise.reject("No key '" + fromOther + "' in ViewParams.Other");
    }

    public ModalSearch(modelId: string, term: string) { }

    protected GetLookupOptions(): any | null {
        if (this.lookupsContainer) {
            var s = this.GetObjectFromHtml(this.lookupsContainer);

            return s;
        }
        return null;
    }

    ngOnInit(): void {

    }

    Refresh() {
        this.ngOnInit();
    }

    GetParameter(key: string, def: string | null = null): string | null {
        var val = this.ViewParams.Other[key];
        if (val && val.length > 0)
            return this.ViewParams.Other[key];
        else
            return def;
    }

    GetParameterAsBoolean(key: string, def: boolean = false): boolean {
        var val = this.GetParameter(key);
        if (val != null) {
            if (val.toLowerCase() == "true")
                return true;
            else if (val.toLowerCase() == "false")
                return false;
        }
        return def;
    }

    GetPermission(resource: string): Permission {
        return Shell.Session.GetPermission(resource);
    }

    Notify(message: string, type: NoteType = NoteType.Success, title: string | undefined = undefined) {
        Shell.Main.Notify(message, type, title);
    }


    NotifyCanNotDeleteRow(res: DeleteResult) {
        //debugger
        let tableName = ""
        if (res.tableName)
            tableName = Shell.Word(res.tableName);
        let mess = Shell.Message("entity_has_children", { "p0": tableName || "Unknown" });
        this.Notify(mess, NoteType.Error, undefined);
    }

    NotifyTranslate(messageId: string, type: NoteType = NoteType.Success, title: string | undefined = undefined) {
        Shell.Main.NotifyTranslate(messageId, type, title);
    }

    OnModalHide(ev: any = null) {
        if (!this._modalOk && !this._modalCancel)
            this.Cancel();

        this._modalOk = false;
        this._modalCancel = false;

    }

    Ok() {
        this._modalOk = true;
        this.OnOk(this).then(e => {
            if (e) {
                this.Show = false;
            }

        })

    }

    Cancel() {
        this._modalCancel = true;
        this.OnCancel(this).then(e => {
            if (e)
                this.Show = false;
        })
    }

    NavigateToComponent(url: string, ext?: NavigationExtras) {
        if (url.length > 0) {
            if (url[0] != "/")
                url = "/" + url;
            this.Router.navigate([url], ext);
        }
    }

    ngOnDestroy(): void {
        for (var i in this._subs) {
            var c = this._subs[i];
            c.destroy();
        }
    }
}