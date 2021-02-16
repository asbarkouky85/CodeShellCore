import { Component, Input, Output, EventEmitter } from "@angular/core";

@Component({ 
    templateUrl: "./search-group.component.html",
    selector: "search-group" })
export class SearchGroup {

    SearchTerm?: string;
    @Output("termChange") ChangeEvent: EventEmitter<string> = new EventEmitter<string>();

    OnSearch() {
        this.ChangeEvent.emit(this.SearchTerm);
    }
}