import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {

  Url: string = environment.controlApiUrl + "api/Notifications";

  constructor(
    private Http: HttpClient,
  ) { }

  getAll(skip: number, take: number) {
    return this.Http.get<{ collection: any[], size: number }>(this.Url, {
      params: {
        skip,
        take
      }
    });
  }

}
