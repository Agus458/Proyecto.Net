import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { DoorDataType } from 'src/app/models/DoorDataType';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DoorsService {
  
  Url: string = environment.controlApiUrl + "api/Doors";

  constructor(
    private Http: HttpClient
  ) { }

  getAll(skip: number, take: number, buildingId: string) {
    return this.Http.get<{ collection: DoorDataType[], size: number }>(this.Url + "/Building/" + buildingId , {
      params: {
        skip,
        take,
      }
    });
  }

  getById(id: string) {
    return this.Http.get<DoorDataType>(this.Url + "/" + id);
  }

  update(id: string, data: any,) {
    return this.Http.put(this.Url + "/" + id, data);
  }

  delete(id: string) {
    return this.Http.delete(this.Url + "/" + id);
  }

  create(data: any) {
    return this.Http.post(this.Url, data);
  }
  
}
