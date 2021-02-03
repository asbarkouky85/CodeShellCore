import { EditComponentBase } from "codeshell/baseComponents";
import { Injectable } from "@angular/core";
import { FilesHttpService } from "codeshell/http";
import { HomeSlidesService } from "../Http";

@Injectable()
export abstract class HomeSlideEditBase extends EditComponentBase {
	Service = new HomeSlidesService();
	image_f?:any;
	Files = new FilesHttpService("");

	Upload = (f: FileList) => this.Files.PostFiles("Upload", f);
}