import { ServerEventListner } from "./serverEventListener";
import { Shell } from "codeshell/shell";
import { Observable } from "rxjs";
import { SessionManager } from 'codeshell/security';
import { Culture } from "codeshell/localization/locale-data";

export class NotificationListenerBase extends ServerEventListner {

    get NotificationsChanged(): Observable<number> {
        return this.Observe<number>("NotificationsChanged");
    }

    async StartWithUser(userId: string): Promise<string> {
        if (this.IsConnected)
            await this.Stop();
        var deviceId = SessionManager.Current.GetDeviceId();
        var lng = Culture.Current.Language;
        var s = await this.connection.start();
        this.connectionId = await this.connection.invoke("SetUserConnectionId", userId, deviceId, lng) as string;
        return this.connectionId;
    }

    async CloseConnectionByUser(userId: string): Promise<ServerEventListner> {
        var deviceId = SessionManager.Current.GetDeviceId();
        var s = await this.connection.invoke("ClearUserConnectionId", userId, deviceId)
        return await this.Stop();
    }
}