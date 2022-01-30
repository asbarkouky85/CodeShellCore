import { BaseComponent, EditComponentBase } from "codeshell/baseComponents";
import { Injectable, ViewChild } from "@angular/core";
import { ListSelectionService } from "codeshell/helpers/ListSelectionService";
import { MessageListBase } from "./MessageListBase";

@Injectable()
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