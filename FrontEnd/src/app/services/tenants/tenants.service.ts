import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { TenantDataType } from 'src/app/models/TenantDataType';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TenantsService {

  Url: string = environment.controlApiUrl + "api/Tenants";

  constructor(
    private Http: HttpClient,
    private Router: Router
  ) { }

  getAll(skip: number, take: number) {
    return this.Http.get<{ collection: TenantDataType[], size: number }>(this.Url, {
      params: {
        skip,
        take
      }
    });
  }

  getById(id: string) {
    return this.Http.get<TenantDataType>(this.Url + "/" + id);
  }

  get() {
    return this.Http.get<TenantDataType[]>(this.Url + "/List");
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
