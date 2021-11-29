import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserDataType } from 'src/app/models/UserDataType';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UsuariosService {

  Url: string = environment.controlApiUrl + "api/Users";

  constructor(
    private Http: HttpClient
  ) { }

  getAll(skip: number, take: number) {
    return this.Http.get<{ collection: UserDataType[], size: number }>(this.Url, {
      params: {
        skip,
        take
      }
    });
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
