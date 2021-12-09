import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import * as signalR from '@microsoft/signalr';
import { environment } from 'src/environments/environment';
import { NotificationsService } from 'src/app/services/notifications/notifications.service';
import { ToastService } from 'src/app/services/toast/toast.service';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.css']
})
export class NotificationsComponent implements OnInit {

  notifications: any[];
  page = 1;
  size: number;

  constructor(
    private modalService: NgbModal,
    private notificationsService: NotificationsService,
    private toastService: ToastService
  ) { }

  ngOnInit(): void {
    this.getNotifications(0, 10);

    const connection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Information)
      .withUrl(environment.controlApiUrl + 'Notify')
      .build();

    connection.start().then(function () {
      console.log('SignalR Connected!');
    }).catch(function (err) {
      return console.error(err.toString());
    });

    connection.on("BroadcastMessage", () => {
      this.toastService.show("Info", "Nueva Notificacion");
      this.getNotifications(0, 10);
    });
  }

  getNotifications(skip: number, take: number) {
    this.notificationsService.getAll(skip, take).subscribe(
      ok => {
        this.notifications = ok.collection;
        this.size = ok.size;
      },
      error => {
        console.log(error);
      }
    );
  }

  open(content: any) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

  onPageChange(pageNum: number): void {
    this.getNotifications((pageNum - 1) * 10, 10);
  }

}
