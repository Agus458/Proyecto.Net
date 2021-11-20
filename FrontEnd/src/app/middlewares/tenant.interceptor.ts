import { Injectable } from '@angular/core';
import {
    HttpEvent, HttpInterceptor, HttpHandler, HttpRequest
} from '@angular/common/http';

import { Observable } from 'rxjs';
import { AuthenticationService } from '../services/authentication/authentication.service';

@Injectable()
export class TenantInterceptor implements HttpInterceptor {
    constructor(private AuthenticationService: AuthenticationService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (request.headers.get("skip"))
            return next.handle(request);

        const clone = request.clone({
            headers: request.headers.set("TenantIdentifier", this.AuthenticationService.getTenant() ?? "")
        });

        return next.handle(clone);
    }
}