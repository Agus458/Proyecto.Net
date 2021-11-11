import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { LoginComponent } from './components/login/login.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { InicioComponent } from './components/inicio/inicio.component';
import { CarruselComponent } from './components/carrusel/carrusel.component';
import { InstitucionesComponent } from './components/instituciones/instituciones.component';
import { NuevainstitucionComponent } from './components/instituciones/nuevainstitucion/nuevainstitucion.component';
import { ReactiveFormsModule } from '@angular/forms';
import { DashBoardComponent } from './components/dash-board/dash-board.component';
import { AuthInterceptor } from './middlewares/auth.interceptor';
import { UsuariosComponent } from './components/usuarios/usuarios.component';
import { EditUsuarioComponent } from './components/usuarios/edit-usuario/edit-usuario.component';
import { TenantInterceptor } from './middlewares/tenant.interceptor';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { PersonasComponent } from './components/personas/personas.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    LoginComponent,
    InicioComponent,
    CarruselComponent,
    InstitucionesComponent,
    NuevainstitucionComponent,
    DashBoardComponent,
    UsuariosComponent,
    EditUsuarioComponent,
    PageNotFoundComponent,
    PersonasComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    NgbModule,
    ReactiveFormsModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: TenantInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
