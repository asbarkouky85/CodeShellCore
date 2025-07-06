export interface IModel {
    id: number;
    state: string;
}

export interface IDictionary<T> {
    [index: string]: T;
}