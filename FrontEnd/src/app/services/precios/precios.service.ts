import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PrecioDataType } from 'src/app/models/PrecioDatatype';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PreciosService {

  Url: string = environment.controlApiUrl + "api/Precio";

  constructor(
    private Http: HttpClient
  ) 
  { }

  getAll(skip: number, take: number) {
    return this.Http.get<{ collection: PrecioDataType[], size: number }>(this.Url, {
      params: {
        skip,
        take
      }
    });
  }

  getList(){
    return this.Http.get<PrecioDataType[]>(this.Url + "/List");
  }

  getById(id: string) {
    return this.Http.get<PrecioDataType>(this.Url + "/" + id);
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
