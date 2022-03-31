import { EditComponentBase } from "./editComponentBase";
import { SectionedFormService } from "codeshell/helpers/sectionedFormService";
import { Input } from "@angular/core";

export abstract class SectionedEditComponentBase extends EditComponentBase {

    @Input("manager")
    SF = new SectionedFormService(1);
}