
export class Permission {
    public actions: string[];
    public insert: boolean = false;
    public update: boolean = false;
    public delete: boolean = false;
    public details: boolean = false;
    public view: boolean = true;

    constructor() {
        this.actions = [];
    }

    public static get Anonymous(): Permission {
        return Object.assign(new Permission, {
            insert: true,
            update: false,
            delete: false,
            details: false,
            view: true
        });
    }

    public static get Denied(): Permission {
        return Object.assign(new Permission, {
            insert: false,
            update: false,
            delete: false,
            details: false,
            view: false
        });
    }

    public static get FullAccess(): Permission {
        return Object.assign(new Permission, {
            insert: true,
            update: true,
            delete: true,
            details: true,
            view: true
        });
    }

    get canSubmit(): boolean { return this.insert || this.update || this.delete; }

    public Can(ac: number | string): boolean {
        if (ac == "anonymous" || ac == "allowAll")
            return true;

        switch (ac) {
            case 4:
                return this.delete;

            case 2:
                return this.update;

            case 3:
                return this.insert;

            case 1:
                return this.details;

            case 0:
                return this.view;

            default:
                if (this.actions == null)
                    return false;
                return this.actions.indexOf(ac as string) > -1;
        }
    }
}