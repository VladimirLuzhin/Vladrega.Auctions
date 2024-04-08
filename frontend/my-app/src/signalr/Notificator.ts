import { HubConnectionBuilder, HubConnection, HubConnectionState, HttpTransportType } from '@microsoft/signalr'
import { Bet } from '../models/Bet'

const methods = {
    onDoBet: 'OnDoBet'
};

type OnDoBetNotification = {
    lotId: string,
    bets: Bet[]
}

class Notificator {
    private readonly connection: HubConnection;

    constructor() {
        this.connection = new HubConnectionBuilder()
            .withUrl('http://localhost:5217/notifications', HttpTransportType.WebSockets)
            .withAutomaticReconnect()
            .build();
    }

    public startAsync(): Promise<void> {
        if (this.connection.state !== HubConnectionState.Disconnected)
            return Promise.resolve();

        return this.connection.start()
    }

    public stopAsync() : Promise<void> {
        return this.connection.stop();
    }

    public subscribeOnBets(callback: (lotId: string, bets: Bet[]) => void) : void {
        this.connection.on(methods.onDoBet, (notification: OnDoBetNotification) => {
            callback(notification.lotId, notification.bets)
        })
    }

    public unsubscribeOnBets(callback: (lotId: string, bets: Bet[]) => void) {
        this.connection.off(methods.onDoBet, callback)
    }
}

export const notificator = new Notificator()