import { Component, Input, Output, EventEmitter } from "@angular/core";

@Component({ 
    selector: "paginate", 
    templateUrl:"./paginate.component.html"
})
export class Paginate {
    private totalPages: number = 0;

    private _current: number = 0;
    private _max: number = 0;
    Pages: Page[] = [];
    @Input("showing") Showing: number = 10;
    @Input("total-count") Total: number = 0;

    @Input("currentPage")
    get Current(): number { return this._current; }
    set Current(val: number) {
        this._current = val;

        this.currentPageChange.emit(this._current);
    }

    @Input("max-pages") MaxPages?: number;
    @Output("pageChange") PageChange: EventEmitter<number> = new EventEmitter<number>();

    @Output("currentPageChange") currentPageChange: EventEmitter<number> = new EventEmitter<number>();

    ngOnInit() {
        if (this.Showing != 0) {
            this.SetPages();
        }


    }

    ngOnChanges() {

        this.SetPages();
    }

    SelectPage(p: Page) {
        if (this.Current == p.id)
            return;
        this.Current = p.id;

        if (this.PageChange != undefined) {
            this.PageChange.emit(this.Current);
            //this.CurrentOut.emit(this.Current);
        }
    }

    Prev() {
        if (this.Current > 0) {
            this.Current -= 1;
            this.PageChange.emit(this.Current);
            //this.CurrentOut.emit(this.Current);
        }

    }

    Next() {
        if (this.Current < (this.totalPages - 1)) {
            this._current += 1;
            this.PageChange.emit(this.Current);
            //this.CurrentOut.emit(this.Current);
        }
    }

    SetPages() {
        ////debugger;
        
        this.Pages = [];
        if (this.Showing == 0 || this.Total == 0)
            return;
        let cnt = this.Total / this.Showing;
        let num: number = Math.floor(cnt);
        if (num < cnt)
            num += 1;
        var reset = this.Current > (num-1);

        this.totalPages = num;
        if (reset)
            this.SelectPage({ id: 0, name:"1" });
        this.WritePages();


    }

    WritePages() {
        if (this.MaxPages) {
            ////debugger;
            let s = this.MaxPages / 2;
            let sInt = Math.floor(s);
            let start = 0, end = this.MaxPages;
            let noOfPages = this.MaxPages > this.totalPages ? this.totalPages : this.MaxPages;
            start = this.Current - sInt;
            start = start < 0 ? 0 : start;
            end = start + noOfPages;

            if ((this.totalPages - 1) < end) {

                end = this.totalPages - 1;
                start = end - noOfPages;
                start = start < 0 ? 0 : start;
            }
            for (let i = start; i <= end; i++)
                this.Pages.push({ id: i, name: (i + 1).toString() });
        } else {

            for (let i = 0; i < this.totalPages; i++)
                this.Pages.push({ id: i, name: (i + 1).toString() });
        }
    }
}

class Page {
    id: number = 0;
    name: string = "";

    constructor(data: any) {
        Object.assign(this, data);
    }
}