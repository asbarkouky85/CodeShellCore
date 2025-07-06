import { HubConnection, HubConnectionBuilder, HubConnectionState } from "@microsoft/signalr";
import { Observable } from "rxjs";
import { EventEmitter } from "@angular/core";
import { Shell } from "codeshell/shell";
import { ServerConfigBase } from "codeshell/serverConfigBase";
import { Utils } from "codeshell/utilities";


export class ServerEventListner {

    get IsConnected() { return this.connection.state == HubConnectionState.Connected; }

    connection: HubConnection;
    connectionId: string = "";
    Closed: Observable<string> = new EventEmitter<string>();
    Reconnected: Observable<ServerEventListner> = new EventEmitter<ServerEventListner>();
    KeepAlive: boolean = false;
    /** if keepAlive is true the interval between reconnect attempts */
    RetryTime: number = 30000;
    private _observables: { [key: string]: EventEmitter<any> } = {};
    private _interval: NodeJS.Timeout | null = null;

    protected _isStarted: boolean = false;
    constructor(private hubUrl: string) {
        let conf = Shell.Injector.get<ServerConfigBase>(ServerConfigBase);
        let url = Utils.Combine(conf.ApiUrl, hubUrl);
        this.connection = new HubConnectionBuilder().withUrl(url).build();
        this.connection.serverTimeoutInMilliseconds = 12000000;
        this.connection.onclose(d => {
            this._isStarted = false;
            (this.Closed as EventEmitter<string>).emit(this.connectionId);
            this.connectionId = "";
            if (this.KeepAlive && this._interval == null) {
                this._interval = setInterval(() => {
                    console.log("Attempting to reconnect");
                    if (!this._isStarted && this.connection.state == HubConnectionState.Disconnected) {
                        this.Start().then(d => {
                            clearInterval(this._interval);
                            (this.Reconnected as EventEmitter<ServerEventListner>).emit(this);
                            this._interval = null;
                        }).catch(d => {
                            console.log("Failed to reconnect")
                        })
                    } else if (this._interval) {

                        clearInterval(this._interval);
                        this._interval = null;
                    }
                }, this.RetryTime)
            }
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

    async Stop(): Promise<ServerEventListner> {
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