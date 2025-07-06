import { Directive, ViewContainerRef, ComponentFactoryResolver, ComponentRef, Input, Injector } from "@angular/core";
import { BaseComponent } from "../base-components/base-component";

@Directive({ selector: "ng-template[acs-component-loader]", exportAs: "componentLoader" })
export class ComponentLoader {
    constructor(public Container: ViewContainerRef, private inj: Injector, private FactoryResolver: ComponentFactoryResolver) { }

    CreateComponent<T extends BaseComponent>(e: new (...r: any) => T): ComponentRef<T> {
        var fac = this.FactoryResolver.resolveComponentFactory(e);
        let ref: ComponentRef<T> = fac.create(this.inj);
        return ref;
    }

    UseComponent<T extends BaseComponent>(ref: ComponentRef<T>) {
        if (this.Container.indexOf(ref.hostView) == -1) {
            this.Container.insert(ref.hostView);
        }
    }

    Clear() {
        this.Container.clear();
    }
}