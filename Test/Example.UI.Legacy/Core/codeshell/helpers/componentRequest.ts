import { BaseComponent } from "../baseComponents";

export class ComponentRequest<T extends BaseComponent> {
    Identifier: string = "";
    DefaultComponent?: string = "";
    
    Init?: (comp: T) => void;
}