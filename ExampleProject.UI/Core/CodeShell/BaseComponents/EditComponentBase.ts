import { BaseComponent } from "./BaseComponent";
import { DataHttpService } from "CodeShell/Http";
import { NoteType, SubmitResult, Result } from "CodeShell/Helpers";
import { Shell } from "CodeShell/Shell";
import { Router } from "@angular/router";
import { ViewChild, EventEmitter } from "@angular/core";
import { NgForm } from "@angular/forms";
import { LocalizablesDTO } from "CodeShell/Helpers/LocalizablesDTO";

export abstract class EditComponentBase extends BaseComponent {

    model: any = {};
    @ViewChild("Form") Form?: NgForm;
    CurrentLang: string = "ar";

    UseLocalization: boolean = false;
    Localizables: { [key: string]: LocalizablesDTO } = {};

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


    abstract get Service(): DataHttpService;

    ngOnInit() {

        this.model = this._bound ? this._bound : this.DefaultModel();

        let lookupOpts = this.GetLookupOptions();

        if (lookupOpts == null && !this.LoadLookups) {
            this.StartComponent();
        } else {
            this.LoadLookupsAsync(lookupOpts).then(lookups => {
                this.Lookups = lookups;
                this.StartComponent();
            });
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

    Fill(id: number) {

        this.GetModelFromServerAsync(id).then(v => {
            this.model = v;
            this.IsInitialized = true;
            this.IsNew = false;
            setTimeout(() => this.SetAccessibility(), 700);
            this.OnReady();
        });
        if (this.UseLocalization) {
            this.Service.GetLocalizationData(id).then(s => {
                this.Localizables = s
            })
        }
    }

    Bind(item: any) {
        this._bound = item;

        if (this.IsInitialized) {
            this.model = item;
            setTimeout(() => this.SetAccessibility(), 500);
            this.OnReady();
        }

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
        //debugger
        var err = JSON.parse(res._body);
        if (err.code == 547)
            this.NotifyCanNotDeleteRow(err);
        else
        this.Notify("Failed", NoteType.Error);
    }


}