import { Directive, Input, ElementRef, Output, EventEmitter, OnChanges, HostListener } from "@angular/core";
import { TmpFileData } from "../results";

@Directive({ selector: "[file-uploader]", exportAs: "[file-uploader]" })
export class FileUploader implements OnChanges {


    @Input("multiple")
    multiple: boolean = false;

    @Input("file-uploader")
    Uploader?: (lst: FileList) => Promise<TmpFileData[]>;

    @Input("fileData")
    get FileData(): TmpFileData | undefined | null { return this._fileData; }
    set FileData(data: TmpFileData | undefined | null) { this._fileData = data; }

    @Output("fileDataChange")
    FileDataChange: EventEmitter<TmpFileData | null> = new EventEmitter<TmpFileData | null>();

    @Input("fileDataMany")
    get fileDataMany(): TmpFileData[] { return this._fileDataMany; }
    set fileDataMany(data: TmpFileData[]) { this._fileDataMany = data; }

    @Output("fileDataManyChange")
    fileDataManyChange: EventEmitter<TmpFileData[]> = new EventEmitter<TmpFileData[]>();

    @HostListener("dragover", ["$event"])
    OnDragOver(event: DragEvent) {

        event.stopPropagation();
        event.preventDefault();
    }

    @HostListener("drop", ["$event"])
    OnDrop(event: DragEvent) {
        if (event.dataTransfer) {
            this._uploadTmp(event.dataTransfer.files)
        }
        event.preventDefault();
    }

    @HostListener("change", ["$event"])
    OnChange(ev: any) {
        if (!this._fileInput)
            return;
        if (this.Element.files) {
            this._uploadTmp(this.Element.files);
        }
        if (this.Element)
            this.Element.value = "";
    }

    private _fileData?: TmpFileData | null = null;
    private _fileDataMany: TmpFileData[] = [];
    private _fileInput = false;

    Element: HTMLInputElement;

    constructor(elem: ElementRef) {
        var el = elem.nativeElement as HTMLInputElement;
        if (el.type && el.type == "file") {
            this._fileInput = true;
        }
        this.Element = elem.nativeElement;
    }

    private _uploadTmp(files: FileList) {
        if (this.Uploader) {
            this.Uploader(files).then(d => {

                if (this.multiple) {
                    this._fileDataMany = d;
                    this.fileDataManyChange.emit(this._fileDataMany);
                } else if (d.length > 0) {
                    this._fileData = d[0];
                    this.FileDataChange.emit(this._fileData);
                } else {
                    this._fileData = null;
                    this.FileDataChange.emit(null);
                }
            }).catch(e => {
                this._fileData = null;
                this.FileDataChange.emit(null);
            })
        }
        this.Element.value = "";
    }



    ngOnChanges(changes: any): void {

    }


}