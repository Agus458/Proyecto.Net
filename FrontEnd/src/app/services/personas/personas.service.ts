import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ProyectConfig } from 'proyectConfig';
import { PersonDataType } from 'src/app/models/PersonDataType';


@Injectable({
  providedIn: 'root'
})
export class PersonasService {

  Url: string = ProyectConfig.ControlApiUrl + "api/Persons";

  constructor(
    private Http: HttpClient
  ) { }

  getAll() {
    return this.Http.get<PersonDataType[]>(this.Url);
  }

  getById(id: string) {
    return this.Http.get<PersonDataType>(this.Url + "/" + id);
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
