import * as Moment from "moment";

export function absUrl(url: string | null | undefined): string {
    if (url) {
        if (url.length > 0) {
            if (url[0] != "/")
                url = "/" + url;
        }
        return url;
    }
    return "/";
}


export function String_GetBeforeLast(data: string, del: string): string {
    var x = data.lastIndexOf(del);
    return data.substr(0, x);
}

export function String_GetAfterLast(data: string, del: string): string {
    var x = data.lastIndexOf(del);
    if (x == -1)
        return data;
    return data.substr(x + del.length);
}

export function Date_Get(ev: Date | string): Date {
    let inp: Date = ev as Date;

    if (typeof ev == "string")
        inp = new Date(Date.parse(ev as string));


    return inp;
}

export function Date_Elapsed(start: Date, end: Date): Moment.Duration | null {
    if (end < start)
        return null;

    let endMoment = Moment(end);
    let startMoment = Moment(start);

    return Moment.duration(endMoment.diff(startMoment));
}