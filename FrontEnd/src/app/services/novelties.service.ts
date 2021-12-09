import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { NoveltiesDataType } from '../models/NoveltiesDataType';

@Injectable({
  providedIn: 'root'
})
export class NoveltiesService {
  Url: string = environment.controlApiUrl + "api/Novelties";

  constructor(
    private Http: HttpClient
  ) { }

  getAll(skip: number, take: number, buildingId: string) {
    return this.Http.get<{ collection: NoveltiesDataType[], size: number }>(this.Url + "/Building/" + buildingId , {
      params: {
        skip,
        take,
      }
    });
  }

  getById(id: string, buildingId: string) {
    return this.Http.get<NoveltiesDataType>(this.Url + "/Building/" + buildingId + "/" + id);
  }

  update(id: string, data: any) {
    let form = new FormData();

    Object.keys(data).forEach(key => {
      form.append(key, data[key]);
    });

    return this.Http.put(this.Url + "/" + id, form);
  }

  delete(id: string, buildingId: string) {
    return this.Http.delete(this.Url + "/Building/" + buildingId + "/" + id);
  }

  create(data: any) {
    let form = new FormData();

    Object.keys(data).forEach(key => {
      form.append(key, data[key]);
    });

    return this.Http.post(this.Url, form);
  }
  
}