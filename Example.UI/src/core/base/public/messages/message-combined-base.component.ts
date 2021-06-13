import { Component, ViewChild } from "@angular/core";
import { BaseComponent, EditComponentBase } from "codeshell/base-components";
import { ListSelectionService } from "codeshell/services";
import { MessageListBase } from "./message-list-base.component";

@Component({template:''})
export abstract class MessageCombinedBase extends BaseComponent {
    HideNav = true;
    HideHeader = true;

    navSection = "Complaints";

    @ViewChild("list_component")
    list_component?: MessageListBase;

    @ViewChild("edit_component")
    edit_component?: EditComponentBase;

    Selection = new ListSelectionService();

    ngOnInit() {
        super.ngOnInit();

        if (this.list_component) {
            this.list_component.OnSelected = e => {
                if (this.edit_component)
                    this.edit_component.FillAsync(e.id);
            }
            this.list_component.LoadData();
        }

        if(this.edit_component){
            this.edit_component.DataSubmitted=res=>{
                if(this.list_component)
                this.list_component.LoadData();
            }
        }
    }
}