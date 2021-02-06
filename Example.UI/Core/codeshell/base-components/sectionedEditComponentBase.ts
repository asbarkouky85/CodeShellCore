import { EditComponentBase } from "./editComponentBase";
import { SectionedFormService } from "codeshell/services/sectionedFormService";
import { Input } from "@angular/core";

export abstract class SectionedEditComponentBase extends EditComponentBase {

    @Input("manager")
    SF = new SectionedFormService(1);
}