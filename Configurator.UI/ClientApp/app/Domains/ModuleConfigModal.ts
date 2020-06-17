import { Component } from "@angular/core";
import { ModulesConfigBase } from "Example/PageCategories/ModulesConfigBase";

@Component({ templateUrl : "./ModuleConfigModal.html", selector : "moduleConfigModal" })
export class ModuleConfigModal extends ModulesConfigBase {
	public GetPageId() : number { return 2011301094000; }
	public get CollectionId(): string | null { return null; }
}