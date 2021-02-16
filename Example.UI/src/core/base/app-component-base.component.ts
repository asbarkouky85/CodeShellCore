import { Injectable } from "@angular/core";
import { IAppComponent } from "codeshell/base-components";
import { ComponentLoader } from "codeshell/directives";

@Injectable()
export class AppComponentBase extends IAppComponent {
    ModalLoader?: ComponentLoader | undefined;    
}