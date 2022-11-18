import { Inject, Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject, lastValueFrom, Subject } from 'rxjs';
import { SnackbarService } from './snackbar.service';
import { TaskUnitStatus } from '../models/common.model';
import { AuthService } from './auth.service';

@Injectable({ providedIn: 'root' })
export class SignalrService {
  private msgSignalrSource = new Subject();
  msgReceived$ = this.msgSignalrSource.asObservable();
  isAuthenticated: BehaviorSubject<boolean> = new BehaviorSubject(false);

  constructor(
    @Inject('SIGNALR_HUB_URL') private signalRHubUrl: string,
    private snackbarService: SnackbarService,
    private authService: AuthService
  ) {
    this.authService.isAuthenticated$.subscribe((isAuthenticated) => {
      if (isAuthenticated) {
        let hubConnection = new signalR.HubConnectionBuilder()
          .withUrl(`${this.signalRHubUrl}/hub/notificationhub`, {
            accessTokenFactory: async () =>
              await lastValueFrom(this.authService.getAccessToken()),
          })
          .configureLogging(signalR.LogLevel.Information)
          .withAutomaticReconnect()
          .build();

        this.establishConnection(hubConnection);
        this.registerHandlers(hubConnection);
      }
    });
  }

  establishConnection(hubConnection: signalR.HubConnection): void {
    hubConnection
      .start()
      .then(() => {
        console.log('Hub connection started');
      })
      .catch(() => {
        console.log('Error while establishing connection');
      });
  }

  private registerHandlers(hubConnection: signalR.HubConnection) {
    hubConnection.on('UpdatedTaskState', (msg) => {
      this.snackbarService.success(
        `Task (${msg.taskId}) status updated to ${
          TaskUnitStatus[msg.taskStatus]
        }`
      );
      this.msgSignalrSource.next(void 0);
    });
  }
}
