import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ProyectConfig } from 'proyectConfig';
import { TenantDataType } from 'src/app/models/TenantDataType';

@Injectable({
  providedIn: 'root'
})
export class TenantsService {

  Url: string = ProyectConfig.ControlApiUrl + "api/Tenants";

  constructor(
    private Http: HttpClient,
    private Router: Router
  ) { }

  getAll() {
    return this.Http.get<TenantDataType[]>(this.Url);
  }

  getById(id: string) {
    return this.Http.get<TenantDataType>(this.Url + "/" + id);
  }
}
