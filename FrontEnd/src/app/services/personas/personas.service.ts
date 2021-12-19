import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PersonDataType } from 'src/app/models/PersonDataType';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PersonasService {

  Url: string = environment.controlApiUrl + "api/Persons";

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

  csv(file: File) {
    const formData = new FormData();

    formData.append("file", file);

    return this.Http.post(this.Url + "/CSV", formData);
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

  identify(image: File) {
    let form = new FormData();
    form.append("fileImage", image);

    return this.Http.post<PersonDataType>(this.Url + "/Identify", form);
  }
}
