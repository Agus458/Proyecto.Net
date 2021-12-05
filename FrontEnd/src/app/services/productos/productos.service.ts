import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ProductosDataType } from 'src/app/models/ProductosDataType';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductosService {

  Url: string = environment.controlApiUrl + "api/Productos";

  constructor(
    private Http: HttpClient
  ) { }

  getAll(skip: number, take: number) {
    return this.Http.get<{ collection: ProductosDataType[], size: number }>(this.Url, {
      params: {
        skip,
        take
      }
    });
  }

  getList(){
    return this.Http.get<ProductosDataType[]>(this.Url + "/List");
  }

  getById(id: string) {
    return this.Http.get<ProductosDataType>(this.Url + "/" + id);
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
