import { AnimationTriggerMetadata } from "@angular/animations";
import { Injectable } from "@angular/core";

@Injectable()
export abstract class IAnimationContainer {

    abstract GetAnimations(comp: string): AnimationTriggerMetadata[];
}