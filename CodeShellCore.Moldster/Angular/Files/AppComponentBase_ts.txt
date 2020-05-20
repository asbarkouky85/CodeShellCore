import { ViewChild } from "@angular/core";
import { IAppComponent } from "codeshell/baseComponents";
import { TestLoader } from "codeshell/core";

export class AppComponentBase extends IAppComponent {

    @ViewChild(TestLoader)
    ModalLoader?: TestLoader | undefined;
    
}