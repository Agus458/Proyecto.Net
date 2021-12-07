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

  getAll(buildingId: string) {
    return this.Http.get<EventDataType[]>(this.Url + "/Building/" + buildingId);
  }

  getById(id: string, buildingId: string) {
    return this.Http.get<EventDataType>(this.Url + "/" + id, { params: { buildingId } });
  }

  delete(id: string, buildingId: string) {
    return this.Http.delete(this.Url + "/" + id, { params: { buildingId } });
  }

  create(data: any) {
    return this.Http.post(this.Url, data);
  }

  update(id: string, buildingId: string, data: any) {
    return this.Http.put(this.Url + "/Building/" + buildingId + "/" + id, data);
  }
}
