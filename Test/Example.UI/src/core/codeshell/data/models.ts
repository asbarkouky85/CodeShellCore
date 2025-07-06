export interface IModel {
    id: number;
    state: string;
}

export interface IHasLocation {
    longitude?: number;
    latitude?: number;
}

export interface IDTO {
    entity: any;
}

export class DTO<T>{
    entity: T = {} as T;
}

