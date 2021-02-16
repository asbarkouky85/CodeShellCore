import { BaseComponent } from "../base-components";

export class ComponentRequest<T extends BaseComponent> {
    Identifier: string = "";
    DefaultComponent?: string = "";
    
    Init?: (comp: T) => void;
}