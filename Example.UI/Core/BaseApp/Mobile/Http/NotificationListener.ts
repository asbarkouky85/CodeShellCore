import { ServerEventListner } from "codeshell/http/serverEventListener";

import { Observable } from "rxjs";
import { Shell } from "codeshell/core";
import { NotificationListenerBase } from "codeshell/http/notificationListenerBase";

export class NotificationListener extends NotificationListenerBase {


    constructor() {
        super("/notificationsHub");
    }

    
}