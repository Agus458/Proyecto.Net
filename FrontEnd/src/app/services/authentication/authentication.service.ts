import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  Url: string = environment.controlApiUrl + "api/Authentication";

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

  getSocialReason(): string | null {
    const socialReason = localStorage.getItem("socialReason");

    if (socialReason) return JSON.parse(socialReason);

    return null;
  }


  isLogged(): boolean {
    return localStorage.getItem("email") != null;
  }

  login(email: string, password: string, tenant: string): void {
    this.Http.post<any>(this.Url + "/Login", { email, password, socialReason: tenant }).subscribe(
      result => {
        localStorage.setItem("token", result.token);
        localStorage.setItem("roles", JSON.stringify(result.roles));
        localStorage.setItem("email", JSON.stringify(result.email));
        localStorage.setItem("tenant", JSON.stringify(result.tenant));
        localStorage.setItem("socialReason", JSON.stringify(result.socialReason));

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
    this.Router.navigateByUrl("/");
  }
}
