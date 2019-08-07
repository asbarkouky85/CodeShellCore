export class Stored {

    public static Set<T>(index: string, item:T) {
        let s = JSON.stringify(item);
        localStorage.setItem(index, s);
    }

    public static Clear(index: string) {
        localStorage.removeItem(index);
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
}