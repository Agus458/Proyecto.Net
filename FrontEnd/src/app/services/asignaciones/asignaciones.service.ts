import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AssignmentDataType } from 'src/app/models/AssignmentDataType';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AsignacionesService {

  Url: string = environment.controlApiUrl + "api/Assignments";

  constructor(
    private Http: HttpClient
  ) { }

  getAll(skip: number, take: number) {
    return this.Http.get<{ collection: AssignmentDataType[], size: number }>(this.Url, {
      params: {
        skip,
        take
      }
    });
  }

  getById(id: string) {
    return this.Http.get<AssignmentDataType>(this.Url + "/" + id);
  }
  
  create(door: string) {
    return this.Http.post(this.Url, door);
  }
}
