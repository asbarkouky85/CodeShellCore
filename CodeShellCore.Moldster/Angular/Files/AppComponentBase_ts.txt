import { Injectable } from "@angular/core";
import { AppBaseComponent } from "codeshell/base-components";
import { ComponentLoader } from "codeshell/directives";

@Injectable()
export class AppComponentBase extends AppBaseComponent {
    ModalLoader?: ComponentLoader | undefined;    
}