import { Component } from "@angular/core";
import { ResourcesService } from "@base/http";
import { Shell } from "codeshell";
import { EditComponentBase } from "codeshell/base-components";

@Component({ template: '' })
export abstract class ResourceEditBase extends EditComponentBase {
	Service = new ResourcesService();
}