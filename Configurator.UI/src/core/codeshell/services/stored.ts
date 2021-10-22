export enum StorageType { Local, Session }
export class Stored {

    public static Set<T>(index: string, item: T, type: StorageType = StorageType.Local) {
        let s = JSON.stringify(item);
        switch (type) {
            case StorageType.Local:
                localStorage.setItem(index, s);
                break;
            case StorageType.Session:
                sessionStorage.setItem(index, s);
                break;
        }

    }

    public static Clear(index: string, type: StorageType = StorageType.Local) {
        switch (type) {
            case StorageType.Local:
                localStorage.removeItem(index);
                break;
            case StorageType.Session:
                sessionStorage.removeItem(index);
                break;
        }

    }

    private static _toObject<T>(item: string | null, exp: new (...params: any[]) => T): T | null {
        if (item == undefined || item == null)
            return null;

        try {
            let d: T = new exp;
            let ob = JSON.parse(item);
            Object.assign(d, ob);

            return d;

        } catch (e) {
            return null;
        }
    }

    public static Get<T>(index: string, exp: new (...params: any[]) => T, type: StorageType = StorageType.Local): T | null {
        let item = null;

        switch (type) {
            case StorageType.Local:
                item = localStorage.getItem(index);
                break;
            case StorageType.Session:
                item = sessionStorage.getItem(index);
                break;
        }

        return this._toObject(item, exp);
    }
}