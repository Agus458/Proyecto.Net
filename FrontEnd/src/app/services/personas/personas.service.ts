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

  getAll(skip: number, take: number) {
    return this.Http.get<{ collection: PersonDataType[], size: number }>(this.Url);
  }

  getById(id: string) {
    return this.Http.get<PersonDataType>(this.Url + "/" + id);
  }

  update(id: string, data: any) {
    let form = new FormData();
    
    Object.keys(data).forEach(key => {
      form.append(key, data[key]);
    });

    return this.Http.put(this.Url + "/" + id, form);
  }

  delete(id: string) {
    return this.Http.delete(this.Url + "/" + id);
  }

  create(data: any) {
    let form = new FormData();
    
    Object.keys(data).forEach(key => {
      form.append(key, data[key]);
    });

    return this.Http.post(this.Url, form);
  }
  
}
