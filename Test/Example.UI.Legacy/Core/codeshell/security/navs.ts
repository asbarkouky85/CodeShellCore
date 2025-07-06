import { RouteData } from "./models";

export class ModuleItem {
    public name: string = "";
    public identifier: string = "";
    public children: FunctionItem[] = [];
    public active: boolean = false;
}

export class FunctionItem {
    public name: string = "";
    public url: string = "";
}