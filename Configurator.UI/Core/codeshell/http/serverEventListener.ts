import { HubConnection, HubConnectionBuilder } from "@aspnet/signalr";
import { Observable } from "rxjs";
import { EventEmitter } from "@angular/core";


export class ServerEventListner {
    connection: HubConnection;
    connectionId: string = "";
    OnClosed: Observable<string> = new EventEmitter<string>();
    private _observables: { [key: string]: EventEmitter<any> } = {};

    private _isStarted: boolean = false;
    constructor(private hubUrl: string) {
        this.connection = new HubConnectionBuilder().withUrl(this.hubUrl).build();
        this.connection.onclose(d => {
            this._isStarted = false;
            (this.OnClosed as EventEmitter<string>).emit(this.connectionId);
            this.connectionId = "";
            console.log("Connection closed");
        });
    }

    async Start(): Promise<string> {
        try {
            if (this._isStarted)
                return this.connectionId;
            await this.connection.start();
            this._isStarted = true;
            this.connectionId = await this.connection.invoke("GetConnectionId");
            return this.connectionId;
        } catch (e) {
            console.error(e);
            throw e;
        }
    }

    async Stop(): Promise<ServerEventListner>  {
        var s = await this.connection.stop();
        this.connectionId = "";
        this._isStarted = false;
        return this;
    }

    Observe<T>(eventName: string): Observable<T> {
        if (!this._observables[eventName]) {
            this._observables[eventName] = new EventEmitter<T>();
            this.connection.on(eventName, (res: any) => {

                this._observables[eventName].emit(res);
            });
        }
        return this._observables[eventName];
    }

    ObserveAs<T>(eventName: string, msgType: new () => T): Observable<T> {
        if (!this._observables[eventName]) {
            this._observables[eventName] = new EventEmitter<T>();
            this.connection.on(eventName, (res: any) => {
                var ob = Object.assign(new msgType, res);
                this._observables[eventName].emit(ob);
            });
        }
        return this._observables[eventName];
    }
}