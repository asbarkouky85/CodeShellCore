import { ServerEventListner } from "./serverEventListener";
import { Shell } from "codeshell/shell";
import { Observable } from "rxjs";

export class NotificationListenerBase extends ServerEventListner{

    get NotificationsChanged(): Observable<number> {
        return this.Observe<number>("NotificationsChanged");
    }

    async StartWithUser(userId: string): Promise<string> {
        if (this.IsConnected)
            await this.Stop();
        var deviceId = Shell.Session.GetDeviceId();
        var lng = Shell.Main.Config.Locale;
        var s = await this.connection.start();
        this.connectionId = await this.connection.invoke("SetUserConnectionId", userId, deviceId, lng) as string;
        return this.connectionId;
    }

    async CloseConnectionByUser(userId: string): Promise<ServerEventListner> {
        var deviceId = Shell.Session.GetDeviceId();
        var s = await this.connection.invoke("ClearUserConnectionId", userId, deviceId)
        return await this.Stop();
    }
}