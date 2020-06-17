import { BaseComponent, ListComponentBase } from "../baseComponents";
import { LoadResult, ListItem, TaggedArgs, LoadOptions } from "../helpers";
import { ListSource } from "../data/listSource";
import { EventEmitter } from "@angular/core";

export abstract class SelectComponentBase extends BaseComponent {

    protected defaultLoader = (e: LoadOptions) => Promise.resolve(new LoadResult);
    Multi: boolean = true;
    Source: ListSource = new ListSource(10, e => this.Loader(e));
    private _items: ListItem[] = [];
    SelectHeight: string = "400px";
    SelectionChangedEvent: EventEmitter<ListItem[]> = new EventEmitter<ListItem[]>();

    get Loader(): (opts: LoadOptions) => Promise<LoadResult> { return this.defaultLoader; }
    set Loader(val: (opts: LoadOptions) => Promise<LoadResult>) { this.defaultLoader = val; };

    SelectionChanged() {
        this.SelectionChangedEvent.emit(this.Items);
    }

    get Items(): ListItem[] { return this._items; };
    set Items(value: ListItem[]) {
        this._items = value;
        this.Args.Source = this._items;
    }

    get Args(): TaggedArgs {
        if (!this.Source.TagArguments) {
            this.Source.TagArguments = {
                Data: this.Source.List,
                Source: this.Items,
                Comparer: (d, s) => s.id == d.id,
                CreateNew: d => {
                    return { id: d.id };
                }
            }
        }
        return this.Source.TagArguments as TaggedArgs;
    }

    LoadData() {
        this.Source.LoadData();
    }

    async StartAsync(): Promise<SelectComponentBase> {
        if (!this.Source.LoadedOnce) {
            await this.Source.LoadDataAsync();
        } else {
            this.Source.Retag();
        }
        return this;
    }
}