import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EventDataType } from 'src/app/models/EventDataType';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EventosService {

  Url: string = environment.controlApiUrl + "api/Events";

  constructor(
    private Http: HttpClient
  ) { }

  getAll(roomId: string) {
    return this.Http.get<EventDataType[]>(this.Url + "/Room/" + roomId);
  }

  getByTenant(tenantId: string) {
    return this.Http.get<EventDataType[]>(this.Url + "/Tenant/" + tenantId);
  }

  getById(id: string, roomId: string) {
    return this.Http.get<EventDataType>(this.Url + "/" + id, { params: { roomId } });
  }

  delete(id: string, roomId: string) {
    return this.Http.delete(this.Url + "/" + id, { params: { roomId } });
  }

  create(data: any) {
    return this.Http.post(this.Url, data);
  }

  update(id: string, roomId: string, data: any) {
    return this.Http.put(this.Url + "/Room/" + roomId + "/" + id, data);
  }
}
