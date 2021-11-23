import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ProyectConfig } from 'proyectConfig';
import { BuildingDataType } from 'src/app/models/BuildingDataType';

@Injectable({
  providedIn: 'root'
})
export class BuildingsService {

  Url: string = ProyectConfig.ControlApiUrl + "api/Buildings";

  constructor(
    private Http: HttpClient
  ) { }

  getAll(skip: number, take: number) {
    return this.Http.get<{ collection: BuildingDataType[], size: number }>(this.Url, {
      params: {
        skip,
        take
      }
    });
  }

  getById(id: string) {
    return this.Http.get<BuildingDataType>(this.Url + "/" + id);
  }

  update(id: string, data: any) {
    return this.Http.put(this.Url + "/" + id, data);
  }

  delete(id: string) {
    return this.Http.delete(this.Url + "/" + id);
  }

  create(data: any) {
    return this.Http.post(this.Url, data);
  }
  
}
