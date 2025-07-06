import { Pipe, PipeTransform } from "@angular/core";
import { absUrl } from "codeshell/utilities/utils";

@Pipe({ name: 'absUrl' })
export class AbsoluteUrl implements PipeTransform {
    transform(value: string): string {
        return absUrl(value);
    }
}