import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SalonDataType } from 'src/app/models/SalonDataType';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SalonesService {

  Url: string = environment.controlApiUrl + "api/Rooms";

  constructor(
    private Http: HttpClient
  ) { }

  getAll(skip: number, take: number, buildingId: string) {
    return this.Http.get<{ collection: SalonDataType[], size: number }>(this.Url + "/Building/" + buildingId , {
      params: {
        skip,
        take,
      }
    });
  }

  getById(id: string) {
    return this.Http.get<SalonDataType>(this.Url + "/" + id);
  }

  update(id: string, data: any,) {
    return this.Http.put(this.Url + "/" + id, data);
  }

  delete(id: string) {
    return this.Http.delete(this.Url + "/" + id);
  }

  create(data: any) {
    return this.Http.post(this.Url, data);
  }
}
