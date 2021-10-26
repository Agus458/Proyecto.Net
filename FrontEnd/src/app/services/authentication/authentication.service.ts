import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ProyectConfig } from 'proyectConfig';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  Url: string = ProyectConfig.ControlApiUrl + "api/Authentication";

  constructor(
    private Http: HttpClient,
    private Router: Router
  ) { }

  getToken(): string | null {
    return localStorage.getItem("token");
  }

  getRoles(): string[] | null {
    const roles = localStorage.getItem("roles");

    if (roles) return JSON.parse(roles);

    return null;
  }

  getUser(): string | null {
    const email = localStorage.getItem("email");

    if (email) return JSON.parse(email);

    return null;
  }

  getTenant(): string | null {
    const tenant = localStorage.getItem("tenant");

    if (tenant) return JSON.parse(tenant);

    return null;
  }


  isLogged(): boolean {
    return localStorage.getItem("email") != null;
  }

  login(email: string, password: string, tenant: string): void {
    let headers = new HttpHeaders();
    headers = headers.set("TenantIdentifier", tenant);
    headers = headers.set("skip", "1");

    this.Http.post<any>(this.Url + "/Login", { email, password }, {
      headers: headers
    }).subscribe(
      result => {
        localStorage.setItem("token", result.token);
        localStorage.setItem("roles", JSON.stringify(result.roles));
        localStorage.setItem("email", JSON.stringify(result.email));
        localStorage.setItem("tenant", JSON.stringify(result.tenant));

        this.Router.navigateByUrl("/");
      },
      error => {
        console.log(error);

      }
    );
  }

  hasRole(roleName: string) {
    return this.getRoles()?.find(role => role == roleName);
  }

  logOut() {
    localStorage.clear();
  }
}
