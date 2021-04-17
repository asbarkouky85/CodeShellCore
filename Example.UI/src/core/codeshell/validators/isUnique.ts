import { Directive, Input, HostListener, ElementRef } from "@angular/core";
import { EntityHttpService } from "../http";
import { NgModel } from "@angular/forms";

@Directive({ selector: "[is-unique][ngModel]", exportAs: "is-unique" })
export class IsUnique {

    @Input("data-service") service?: EntityHttpService;
    @Input("column-id") column?: string;
    @Input("item-id") id?: number;

    constructor(private model: NgModel) { }

    @HostListener("blur", ['$event']) Change(arg: any) {
        
        if (this.service && this.column) {
            if (!this.model.value || this.model.value == "") {
                this.model.control.setErrors({ unique: null });
                this.model.control.updateValueAndValidity();
            } else {
                var sil = false;
                if (this.service) {
                    sil = this.service.Silent;
                    this.service.Silent = true;
                }
                    
                this.model.control.setErrors({ unique: true })
                this.service.IsUnique(this.column, this.id, this.model.value).then(e => {
                    if (this.service)
                        this.service.Silent = sil;
                    if (this.model) {
                        if (e) {
                            this.model.control.setErrors({ unique: null });
                            this.model.control.updateValueAndValidity();
                        }
                        else
                            this.model.control.setErrors({ unique: true });
                    }
                });
            }
        }
        
    }
}