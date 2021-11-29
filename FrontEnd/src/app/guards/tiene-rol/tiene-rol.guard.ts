import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class TieneRolGuard implements CanActivate {
  constructor(
    private AuthenticationService: AuthenticationService,
    private router: Router,
  ) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    let roles = route.data.roles as Array<string>;
    const user = this.AuthenticationService.getUser()

    if (user) {
      if (roles.find((rol) => rol == "SuperAdmin") && this.AuthenticationService.hasRole("SuperAdmin")) {
        return true;
      }

      if (roles.find((rol) => rol == "Admin") && this.AuthenticationService.hasRole("Admin")) {
        return true;
      }

      if (roles.find((rol) => rol == "Portero") && this.AuthenticationService.hasRole("Portero")) {
        return true;
      }

      if (roles.find((rol) => rol == "Gestor") && this.AuthenticationService.hasRole("Gestor")) {
        return true;
      }
    }

    console.log("Acceso denegado");


    this.router.navigateByUrl("/");
    return false;
  }

}
