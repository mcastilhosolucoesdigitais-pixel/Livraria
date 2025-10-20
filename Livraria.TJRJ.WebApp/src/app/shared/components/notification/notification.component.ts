import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotificationService, NotificationType, INotification } from '../../../core/services/notification.service';

@Component({
  selector: 'app-notification',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.scss']
})
export class NotificationComponent implements OnInit {
  notifications: (INotification & { id: number })[] = [];
  private notificationId = 0;

  constructor(private notificationService: NotificationService) {}

  ngOnInit(): void {
    this.notificationService.notification$.subscribe(notification => {
      this.showNotification(notification);
    });
  }

  showNotification(notification: INotification): void {
    const id = this.notificationId++;
    const notificationWithId = { ...notification, id };

    this.notifications.push(notificationWithId);

    // Remove notification after duration
    setTimeout(() => {
      this.removeNotification(id);
    }, notification.duration || 3000);
  }

  removeNotification(id: number): void {
    this.notifications = this.notifications.filter(n => n.id !== id);
  }

  getIconClass(type: NotificationType): string {
    switch (type) {
      case NotificationType.Success:
        return 'bi-check-circle-fill';
      case NotificationType.Error:
        return 'bi-x-circle-fill';
      case NotificationType.Warning:
        return 'bi-exclamation-triangle-fill';
      case NotificationType.Info:
        return 'bi-info-circle-fill';
      default:
        return 'bi-info-circle-fill';
    }
  }

  getAlertClass(type: NotificationType): string {
    switch (type) {
      case NotificationType.Success:
        return 'alert-success';
      case NotificationType.Error:
        return 'alert-danger';
      case NotificationType.Warning:
        return 'alert-warning';
      case NotificationType.Info:
        return 'alert-info';
      default:
        return 'alert-info';
    }
  }
}
