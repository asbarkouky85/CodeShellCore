import { ListComponentBase } from "codeshell/baseComponents";
import { Injectable, Output, EventEmitter } from "@angular/core";
import { NavigationGroupsService } from "Example/Http";
import { Shell } from "codeshell/core";
import { NoteType, LoadOptions } from "../../codeshell/helpers";

@Injectable()
export abstract class NaveListBase extends ListComponentBase {
    get Service(): NavigationGroupsService { return Shell.Injector.get(NavigationGroupsService); }

    @Output() valueChange = new EventEmitter();

    options: LoadOptions = { Showing: 0, Skip: 0 };

    selectedRow?: number;
    selectedEdit?: number;
    isEdit: boolean = false;
    isNew: boolean = false;
    tenantId?: number;

    loadPageList(item: any) {
        this.valueChange.emit(item.id);
    }
    model?: any = {};

    setClickedRow(index: number) {
        this.selectedRow = index;
    }

    openEdit(item: any) {
        this.isEdit = true;
        this.selectedEdit = item.id;
    }

    saveEdit(item: any) {
        this.Service.updateNave(item).then(res => {
            this.isEdit = false;
        });
    }

    addNave() {
        this.isNew = true;
    }

    save() {
        
        if (this.model.name == null) {
            this.Notify("Opps! name is required", NoteType.Error, undefined);
            this.isNew = false;
        }
        else {
            this.Service.create(this.model).then(res => {
                console.log(res);
                this.Notify("success! navigation added successfully", NoteType.Success, undefined);
                this.isNew = false;
                this.LoadData();
            }).catch(error => {
                this.Notify("Opps! this navigation added befor", NoteType.Error, undefined);
                this.isNew = true;
            });
        }
    }

    cancel() {
        this.isEdit = false;
    }
}