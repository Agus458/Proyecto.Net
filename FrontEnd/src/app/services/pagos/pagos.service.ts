import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PagoDataType } from 'src/app/models/PagoDataType';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PagosService {

  Url: string = environment.controlApiUrl + "api/Pago";

  constructor(
    private Http: HttpClient
  ) { }

  getAll(skip: number, take: number) {
    return this.Http.get<{ collection: PagoDataType[], size: number }>(this.Url, {
      params: {
        skip,
        take
      }
    });
  }

  getById(id: string) {
    return this.Http.get<PagoDataType>(this.Url + "/" + id);
  }

  // update(id: string, data: any) {
  //   return this.Http.put(this.Url + "/" + id, data);
  // }

  // delete(id: string) {
  //   return this.Http.delete(this.Url + "/" + id);
  // }

  // create(data: any) {
  //   return this.Http.post(this.Url, data);
  // }

  pay(facturaId: string) {
    return this.Http.post<{ url: string }>(environment.controlApiUrl + "api/Payment/Factura/" + facturaId, {});
  }
}
