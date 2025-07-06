import { Component, ViewChild } from "@angular/core";
import { BaseComponent, EditComponentBase } from "codeshell/base-components";
import { HomeSlideListBase } from "./home-slide-list-base.component";

@Component({template:''})
export abstract class HomeSlideCombinedBase extends BaseComponent {

    @ViewChild("list_component")
    list_component?: HomeSlideListBase;

    @ViewChild("edit_component")
    edit_component?: EditComponentBase;

    HideHeader = true;
    HideNav = true;

    ngOnInit() {
        
        if (this.list_component) {
            this.list_component.OnAdding = () => {
                if (this.edit_component) {
                    this.edit_component.Clear();
                }
            }

            this.list_component.OnEditing = m => {
                if (this.edit_component) {
                    this.edit_component.FillAsync(m.id);
                }
            }
            this.list_component.LoadData();
        }

        if (this.edit_component) {
            this.edit_component.DataSubmitted = res => {
                if (this.edit_component)
                    this.edit_component.Clear();
                if (this.list_component)
                    this.list_component.LoadData();
            }
        }
    }

    listOrderChanged(m: any) {
        console.log(m);
    }
}