import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EntryDataType } from 'src/app/models/EntryDataType';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class IngresosService {

  Url: string = environment.controlApiUrl + "api/Entries";

  constructor(
    private Http: HttpClient
  ) { }

  getAll(skip: number, take: number, buildingId: string) {
    return this.Http.get<{ collection: EntryDataType[], size: number }>(this.Url + "/Building/" + buildingId, {
      params: {
        skip,
        take
      }
    });
  }

  get(skip: number, take: number) {
    return this.Http.get<{ collection: EntryDataType[], size: number }>(this.Url, {
      params: {
        skip,
        take
      }
    });
  }

  getById(id: string) {
    return this.Http.get<EntryDataType>(this.Url + "/" + id);
  }

  delete(id: string) {
    return this.Http.delete(this.Url + "/" + id);
  }

  create(data: any) {
    return this.Http.post(this.Url, data);
  }
}
