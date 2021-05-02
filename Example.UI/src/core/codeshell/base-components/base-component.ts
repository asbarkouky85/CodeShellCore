import { OnInit, ElementRef, Input, ComponentRef, OnDestroy, ComponentFactoryResolver, ComponentFactory, Injector, Component } from "@angular/core";
import { ActivatedRoute, Route, Router, NavigationExtras } from "@angular/router";
import { Location } from "@angular/common";

import { Shell } from "../shell";

import { Registry } from "codeshell";
import { ViewParams } from '../moldster';
import { LocalizablesDTO } from '../localization/models';
import { ComponentRequest } from '../components';
import { DeleteResult, NoteType } from '../results';
import { Permission, RouteData, SessionManager } from 'codeshell/security';


@Component({ template: '' })
export abstract class BaseComponent implements OnInit, OnDestroy {
    
    @Input("IsEmbedded") 
    IsEmbedded: boolean = false;

    private _subs: { [key: string]: any } = {};
    private _modalOk = false;
    private _modalCancel = false;

    protected Route: ActivatedRoute;
    protected Injector: Injector;
    protected RouteData: RouteData = new RouteData();
    protected LookupOptions: any | null;
    protected ComponentRouteData: any | null;

    Lookups: { [key: string]: any[] } = {};
    ViewParams: ViewParams = new ViewParams();

    UseLocalization: boolean = false;
    Debug: boolean = true;
    Permission: Permission = Permission.Anonymous;
    ScreenHeight = window.innerHeight + 'px';
    Title: string = "";
    HeaderExtra?: string;
    IsInitialized: boolean = false;
    Show: boolean = false;
    ShowOnReady: boolean = false;
    HideHeader: boolean = false;
    SubmitClicked: boolean = false;
    IsChild = false;
    ModalWidth: any = 768;
    navSection?:any;

    get Navigation(): Location { return Shell.Injector.get(Location); }
    get Loc(): LocalizablesDTO { return new LocalizablesDTO() };
    get ComponentName(): string { return (this as any).constructor.name; }
    get Router(): Router { return Shell.Injector.get(Router); }
    protected get Resolver(): ComponentFactoryResolver { return this.Injector.get(ComponentFactoryResolver); };

    OnOk: (sender: any) => Promise<boolean> = e => Promise.resolve(true);
    OnCancel: (sender: any) => Promise<boolean> = e => Promise.resolve(true);
    EmbeddedInit?: () => void;

    constructor(route: ActivatedRoute, inj: Injector) {
        this.Route = route;
        this.Injector = inj;
    }

    ngOnInit(): void {
        this.construct();
    }

    protected construct() {
        let conf = this.Route.routeConfig as Route;

        if (conf) {

            if (conf.data) {
                this.RouteData = Object.assign(new RouteData, conf.data);
            } else if (this.ComponentRouteData) {
                this.RouteData = Object.assign(new RouteData(), this.ComponentRouteData);
            } else {
                this.RouteData = Object.assign(new RouteData(), { action: "anonymous" })
            }

            Shell.Main.SetTitle(this.RouteData.name);

            if (this.RouteData.IsAnonymous) {
                this.Permission = Permission.FullAccess;
            }
            else if (!SessionManager.Current.IsLoggedIn) {
                Shell.Injector.get(Router).navigate(["/Login"]);
            } else if (this.RouteData.AllowAll) {
                this.Permission = Permission.FullAccess;
            } else {
                this.Permission = SessionManager.Current.GetPermission(this.RouteData.resource);
            }

        } else {
            this.IsEmbedded = true;
        }
    }

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
        return this.LookupOptions;
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
        return SessionManager.Current.GetPermission(resource);
    }

    Notify(message: string, type: NoteType = NoteType.Success, title: string | undefined = undefined) {
        Shell.Main.Notify(message, type, title);
    }


    NotifyCanNotDeleteRow(res: DeleteResult) {
        //debugger
        let tableName = ""
        if (res.tableName)
            tableName = Shell.Word(res.tableName);
        let mess = Shell.Message("entity_has_children", tableName || "Unknown");
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