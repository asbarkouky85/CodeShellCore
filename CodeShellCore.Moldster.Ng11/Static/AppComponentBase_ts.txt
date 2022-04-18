import { AppBaseComponent } from "codeshell/base-components";
import { ComponentLoader } from "codeshell/directives";
import { Component } from "@angular/core";

@Component({ template : "" })
export class AppComponentBase extends AppBaseComponent {
    ModalLoader?: ComponentLoader | undefined;    
}