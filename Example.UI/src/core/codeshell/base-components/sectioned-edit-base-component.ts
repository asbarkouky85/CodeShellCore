import { EditComponentBase } from "./edit-base-component";
import { SectionedFormService } from "codeshell/services/sectionedFormService";
import { Component, Input } from "@angular/core";


@Component({ template: '' })
export abstract class SectionedEditComponentBase extends EditComponentBase {

    @Input("manager")
    SF = new SectionedFormService(1);
}