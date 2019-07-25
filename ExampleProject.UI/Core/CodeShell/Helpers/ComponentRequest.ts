import { BaseComponent } from "../BaseComponents/BaseComponent";

export class ComponentRequest<T extends BaseComponent> {
    Identifier: string = "";
    DefaultComponent?: string = "";
    
    Init?: (comp: T) => void;
}