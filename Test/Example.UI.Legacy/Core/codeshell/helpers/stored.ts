export class Stored {

    public static Set<T>(index: string, item: T) {
        let s = JSON.stringify(item);
        localStorage.setItem(index, s);
    }

    public static Set_SS<T>(index: string, item: T) {
        let s = JSON.stringify(item);
        sessionStorage.setItem(index, s);
    }

    public static Clear(index: string) {
        localStorage.removeItem(index);
    }

    public static Clear_SS(index: string) {
        sessionStorage.removeItem(index);
    }

    public static Get<T>(index: string, exp: new () => T): T | null {
        var item = localStorage.getItem(index);

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

    public static Get_SS<T>(index: string, exp: new () => T): T | null {
        var item = sessionStorage.getItem(index);

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
}