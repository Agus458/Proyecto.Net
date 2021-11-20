import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ProyectConfig } from 'proyectConfig';
import { UserDataType } from 'src/app/models/UserDataType';

@Injectable({
  providedIn: 'root'
})
export class UsuariosService {

  Url: string = ProyectConfig.ControlApiUrl + "api/Users";

  constructor(
    private Http: HttpClient
  ) { }

  getAll() {
    return this.Http.get<UserDataType[]>(this.Url);
  }

  getById(id: string) {
    return this.Http.get<UserDataType>(this.Url + "/" + id);
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
