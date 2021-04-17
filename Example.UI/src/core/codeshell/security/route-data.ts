export enum ResourceActions { view, details, update, insert, delete }
export class RouteData {
    name: string = "";
    navigate: boolean = false;
    resource: string = "";
    action: ResourceActions | string = ResourceActions.view;
    apps?: string[];
    url?: string;

    get IsAnonymous(): boolean { return this.action == "anonymous"; }
    get AllowAll(): boolean { return this.action == "allowAll"; }
}