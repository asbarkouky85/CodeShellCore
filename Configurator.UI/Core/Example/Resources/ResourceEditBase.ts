import { EditComponentBase } from "codeshell/baseComponents";
import { Injectable } from "@angular/core";
import { ResourcesService } from "Example/Http";
import { Shell } from "codeshell/core";

@Injectable()
export abstract class ResourceEditBase extends EditComponentBase{
	get Service(): ResourcesService { return Shell.Injector.get(ResourcesService); }
}