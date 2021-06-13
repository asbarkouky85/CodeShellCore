import { Component } from "@angular/core";
import { EditComponentBase } from "codeshell/base-components";
import { FilesHttpService } from "codeshell/http";
import { HomeSlidesService } from "../http";


@Component({ template: '' })
export abstract class HomeSlideEditBase extends EditComponentBase {
	Service = new HomeSlidesService();
	image_f?: any;
	Files = new FilesHttpService("");
	isActive: { id: boolean, name: string }[] = [];
	UseLocalization = true;

	Upload = (f: FileList) => this.Files.PostFiles("Upload", f);
}