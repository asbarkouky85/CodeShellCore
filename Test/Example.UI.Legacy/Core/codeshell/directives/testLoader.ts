import { Directive, ViewContainerRef, ComponentFactoryResolver, ComponentRef } from "@angular/core";
import { Shell } from "../shell";
import { BaseComponent } from "../baseComponents";
import { ActivatedRoute } from "@angular/router";

@Directive({ selector: "[test-loader]", exportAs: "testLoader" })
export class TestLoader {
    constructor(public Container: ViewContainerRef, private FactoryResolver: ComponentFactoryResolver) { }

    CreateComponent<T extends BaseComponent>(e: new (r: ActivatedRoute) => T): ComponentRef<T> {
        var fac = this.FactoryResolver.resolveComponentFactory(e);
        let ref: ComponentRef<T> = fac.create(Shell.Injector);
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