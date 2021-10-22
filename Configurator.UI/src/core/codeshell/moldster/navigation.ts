import { AuthorizationServiceBase, RouteData, UserDTO } from 'codeshell/security';


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

export class DomainDataProvider {
    Domains: DomainData[] = [];
    constructor(domains: DomainData[]) {
        this.Domains = domains;
    }

    GetByNavGroup(group: string, auth?: AuthorizationServiceBase, user?: UserDTO): FunctionItem[] {

        //var auth: AuthorizationServiceBase = Shell.Injector.get(AuthorizationServiceBase);
        var navs: FunctionItem[] = [];
        var gr = this.Domains.find(e => e.name == group);
        if (gr) {
            for (var c of gr.children) {
                var r: RouteData = Object.assign(new RouteData, c);
                var isAuthorized = auth ? auth.IsAuthorized(user, r) : true;
                if (isAuthorized && r.url) {
                    var item: FunctionItem = {
                        name: r.name,
                        url: r.url
                    }
                    navs.push(item);
                }
            }
        }
        return navs;
    }
}

export class DomainData {
    name: string = "";
    children: any[] = [];
}

