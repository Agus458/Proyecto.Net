import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BuildingDataType } from 'src/app/models/BuildingDataType';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BuildingsService {

  Url: string = environment.controlApiUrl + "api/Buildings";

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

  getList(){
    return this.Http.get<BuildingDataType[]>(this.Url + "/List");
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
