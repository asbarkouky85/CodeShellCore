interface String {
    getBeforeLast(data: string): string;
}

String.prototype.getBeforeLast = function (data: string): string {
    var x = String(this).lastIndexOf(data);
    return String(this).substr(0, x);
}
