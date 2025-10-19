import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';

export enum NotificationType {
  Success = 'success',
  Error = 'error',
  Warning = 'warning',
  Info = 'info'
}

export interface INotification {
  message: string;
  type: NotificationType;
  duration?: number;
}

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private notificationSubject = new Subject<INotification>();
  public notification$: Observable<INotification> = this.notificationSubject.asObservable();

  showSuccess(message: string, duration: number = 3000): void {
    this.notificationSubject.next({
      message,
      type: NotificationType.Success,
      duration
    });
  }

  showError(message: string, duration: number = 5000): void {
    this.notificationSubject.next({
      message,
      type: NotificationType.Error,
      duration
    });
  }

  showWarning(message: string, duration: number = 4000): void {
    this.notificationSubject.next({
      message,
      type: NotificationType.Warning,
      duration
    });
  }

  showInfo(message: string, duration: number = 3000): void {
    this.notificationSubject.next({
      message,
      type: NotificationType.Info,
      duration
    });
  }
}
