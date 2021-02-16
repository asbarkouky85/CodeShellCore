import { EditComponentBase } from "./editComponentBase";
import { SectionedFormService } from "codeshell/services/sectionedFormService";
import { Component, Input } from "@angular/core";


@Component({ template: '' })
export abstract class SectionedEditComponentBase extends EditComponentBase {

    @Input("manager")
    SF = new SectionedFormService(1);
}