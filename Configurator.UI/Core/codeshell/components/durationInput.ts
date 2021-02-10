import { Component, ElementRef, Input, Output, EventEmitter } from "@angular/core";
import { NgModel } from "@angular/forms";

@Component({ templateUrl: "./durationInput.html", selector: "duration-input", exportAs: "duration-input" })
export class DurationInput {

    hours: number = 0;
    minutes: number = 0;
    seconds: number = 0;

    private _model: number = 0;

    @Input("readOnly")
    readOnly: boolean = false;

    @Input("totalSeconds")
    get model(): number { return this._model; };
    set model(val: number) {
        this._model=val;
        this.applyToFields();
    }

    @Output("totalSecondsChange")
    modelChange = new EventEmitter<number>();

    constructor(private el: ElementRef) {
        let html = el.nativeElement as HTMLElement;
    }

    onPartChanged() {
        this._model = this.hours * 60 * 60 + this.minutes * 60 + this.seconds;
        this.modelChange.emit(this._model);
    }

    applyToFields() {
        this.hours = 0;
        this.minutes = 0;
        this.seconds = 0;
        if (this._model >= 3600) {
            this.hours = Math.floor(this._model / (3600));
        }

        if (this._model >= 60) {
            var hrsRem = this._model % 3600;
            this.minutes = Math.floor(hrsRem / 60);
        }

        var minRem = this._model % 60;
        
        this.seconds = Math.floor(minRem);
    }
}