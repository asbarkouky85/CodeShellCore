import { BaseComponent } from "./baseComponent";
import { EntityHttpService } from "../http";
import { NoteType, SubmitResult, Result } from "../helpers";
import { Shell } from "../shell";
import { Router } from "@angular/router";
import { ViewChild, EventEmitter } from "@angular/core";
import { NgForm } from "@angular/forms";
import { LocalizablesDTO } from "../helpers/localizablesDTO";
import { Utils } from "codeshell/utilities/utils";

export abstract class EditComponentBase extends BaseComponent {

    model: any = {};
    @ViewChild("Form") Form?: NgForm;
    CurrentLang: string = "ar";

    UseLocalization: boolean = false;
    Localizables: { [key: string]: LocalizablesDTO } = {};

    _lookupsLoaded: boolean = false;
    isBound: boolean = false;
    LoadLookups: boolean = false;
    IsNew: boolean = true;
    DataSubmitted?: (model: any, result: SubmitResult) => void;
    DataSubmittedEvent: EventEmitter<SubmitResult> = new EventEmitter<SubmitResult>();

    get CanSubmit(): boolean {
        if (this.Form) {
            return this.Form.valid != null && this.Form.valid;
        }
        return true;
    }


    get Loc(): LocalizablesDTO {

        if (!this.Localizables[this.CurrentLang])
            this.Localizables[this.CurrentLang] = new LocalizablesDTO;
        return this.Localizables[this.CurrentLang];
    }

    protected RouteParams: any = {};

    private _bound?: any;


    abstract get Service(): EntityHttpService;

    ngOnInit() {

        this.model = this._bound ? this._bound : this.DefaultModel();

        let lookupOpts = this.GetLookupOptions();

        if (!this.IsEmbedded) {
            if (lookupOpts == null && !this.LoadLookups) {
                this.StartComponent();
            } else {
                this.LoadLookupsAsync(lookupOpts).then(lookups => {
                    this.Lookups = lookups;
                    this.StartComponent();
                });
            }
        }

        this.CurrentLang = Shell.Main.Config.Locale;

    }

    protected DefaultModel(): any { return {}; }

    protected LoadLookupsAsync(opts: any): Promise<any> {
        return this.Service.Get("GetEditLookups", opts);
    }

    protected StartEditOrCreate() {

        if (!this.IsNew) {
            let id = Number.parseInt(this.RouteParams['id']);
            this.Fill(id);

        } else {
            this.StartNew();
        }
    }

    private StartComponent() {

        if (!this.IsEmbedded) {
            this.Route.params.subscribe(params => {
                this.RouteParams = params;
                this.IsNew = this.RouteParams['id'] == undefined;
                this.StartEditOrCreate();
            });
        } else {
            this.IsInitialized = true;
            this.OnReady();

        }
    }

    private async _loadLookupsOnce(): Promise<void> {
        if (this._lookupsLoaded)
            return;
        let lookupOpts = this.GetLookupOptions();
        if (lookupOpts != null || this.LoadLookups) {
            this.Lookups = await this.LoadLookupsAsync(lookupOpts);
        }
        this._lookupsLoaded = true;
    }

    private _componentStarted() {
        this.IsInitialized = true;
        setTimeout(() => this.SetAccessibility(), 700);

        if (this.UseLocalization && this.model && this.model.id != 0) {
            this.Service.GetLocalizationData(this.model.id).then(s => {
                this.Localizables = s
            })
        }

        this.OnReady();
    }

    async FillAsync(id: number): Promise<void> {
        await this._loadLookupsOnce();
        this.model = await this.GetModelFromServerAsync(id);
        this.IsNew = false;
        this._componentStarted();
    }

    async BindAsync(mod: any): Promise<void> {
        await this._loadLookupsOnce();
        this._bound = mod;
        this.model = mod;
        this._componentStarted();
    }

    Fill(id: number) {

        this.GetModelFromServerAsync(id).then(v => {
            this.model = v;
            this.IsNew = false;
            this._componentStarted();
        });
    }

    Bind(item: any) {
        this._bound = item;

        if (this.IsInitialized) {
            this.model = item;
            setTimeout(() => this.SetAccessibility(), 500);
            this.OnReady();
        }
        this.Show = true;
        if (this.UseLocalization && this._bound && this._bound.id != 0) {
            this.Service.GetLocalizationData(this._bound.id).then(s => {
                this.Localizables = s
            })
        }
    }

    StartNew() {
        this.InitializeNewModelAsync().then(() => {
            this.IsInitialized = true;
            setTimeout(() => this.SetAccessibility(), 500);
            this.OnReady();
        });
    }

    SetAccessibility() { }

    /**
     * Is called before submit to check if the form is valid
     * */
    protected Validate(): Result {
        if (this.Form && this.Form.invalid) {
            return { success: false, message: "invalid_form", type: NoteType.Error }
        }
        return { success: true, type: NoteType.Success }
    }

    /**
     * Can be overridden to enable initializing the model on the server side
     * */
    protected async InitializeNewModelAsync(): Promise<void> { }

    /**
     * When an id is found in the route params this method is called to obtain model from server using that id
     * Can be overridden
     * @param id as obtaind from url
     */
    protected GetModelFromServerAsync(id: number): Promise<any> {
        return this.Service.Get("GetSingle", id);
    }

    /**
     * Called after lookups and model is both loaded
     * */
    protected OnReady(): void { };

    async SubmitLocalizablesAsync(): Promise<void> {
        var s: { [key: string]: LocalizablesDTO } = {};
        let submit: boolean = false;
        for (var i in this.Localizables) {
            if (this.Localizables[i].state == "Modified" || this.Localizables[i].state == "Added") {
                s[i] = this.Localizables[i];
                submit = true;
            }
        }
        if (submit) {
            var res = await this.Service.SetLocalizationData(this.model.id, s);
        }
    }

    protected SubmitNewAsync(): Promise<SubmitResult> {
        return this.Service.Post("Post", this.model);
    }

    protected SubmitUpdateAsync(): Promise<SubmitResult> {

        return this.Service.Put("Put", this.model);
    }

    async SubmitAsync(): Promise<SubmitResult> {

        let prom: SubmitResult;


        if (this.IsNew) {
            prom = await this.SubmitNewAsync();
            if (prom.data.Id) {
                this.model.id = prom.data.Id;
                await this.SubmitLocalizablesAsync();
            }
        }
        else {
            prom = await this.SubmitUpdateAsync();
            await this.SubmitLocalizablesAsync();
        }

        return prom;
    }

    Submit() {

        this.SubmitAsync().then(e => {
            this.OnSubmitSuccess(e);
            if (this.DataSubmitted)
                this.DataSubmitted(this.model, e);
            this.DataSubmittedEvent.emit(e);

        }).catch(x => {

            this.OnSubmitFailed(x);
        });
    }

    protected OnSubmitSuccess(res: SubmitResult) {

        this.NotifyTranslate(res.message, NoteType.Success);

        if (!this.IsEmbedded) {
            if (this.ViewParams.ListUrl) {
                let s: Router = Shell.Injector.get(Router);
                s.navigateByUrl(this.ViewParams.ListUrl);
            } else {
                this.Navigation.back();
            }
        } else if (this.model.id) {
            //this.StartEditOrCreate();
        }

    }

    protected OnSubmitFailed(res: any) {
        Utils.HandleError(res, true);
    }


}