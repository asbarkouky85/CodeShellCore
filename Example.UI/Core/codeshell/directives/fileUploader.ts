import { Directive, Input, ElementRef, Output, EventEmitter, OnChanges, HostListener } from "@angular/core";
import { TmpFileData } from "codeshell/helpers";
import { NgModel } from "@angular/forms";

@Directive({ selector: "input[file-uploader]", exportAs: "[file-uploader]" })
export class FileUploader implements OnChanges {

    private _fileData?: TmpFileData | null = null;
    private _fileDataMany: TmpFileData[] = [];

    @Input("multiple")
    multiple: boolean = false;

    @Input("file-uploader")
    Uploader?: (lst: FileList) => Promise<TmpFileData[]>;

    @Input("fileData")
    get FileData(): TmpFileData | undefined | null { return this._fileData; }
    set FileData(data: TmpFileData | undefined | null) { this._fileData = data; }

    @Output("fileDataChange")
    FileDataChange: EventEmitter<TmpFileData|null> = new EventEmitter<TmpFileData|null>();

    @Input("fileDataMany")
    get fileDataMany(): TmpFileData[] { return this._fileDataMany; }
    set fileDataMany(data: TmpFileData[]) { this._fileDataMany = data; }

    @Output("fileDataManyChange")
    fileDataManyChange: EventEmitter<TmpFileData[]> = new EventEmitter<TmpFileData[]>();

    @HostListener("change", ["$event"])
    OnChange(ev: any) {
        if (this.Uploader && this.Element.files) {
            //this.model.viewToModelUpdate(null);
            this.Uploader(this.Element.files).then(d => {
                
                if (this.multiple) {
                    this._fileDataMany = d;
                    this.fileDataManyChange.emit(this._fileDataMany);
                } else if (d.length > 0) {
                    this._fileData = d[0];
                    this.FileDataChange.emit(this._fileData);
                   // this.model.viewToModelUpdate("val");
                } else {
                    this._fileData = null;
                    this.FileDataChange.emit(null);
                    this.model.control.setValue(null);
                   // this.model.viewToModelUpdate("val");
                }
            }).catch(e => {
                this._fileData = null;
                this.FileDataChange.emit(null);
                this.model.control.setValue(null);
              //  this.model.viewToModelUpdate(null);
            })
        }
    }

    Element: HTMLInputElement;

    constructor(elem: ElementRef,private model: NgModel) {
        this.Element = elem.nativeElement;
    }

    ngOnChanges(changes: any): void {

    }


}