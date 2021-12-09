import { LOCALE_ID, NgModule } from '@angular/core';
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
import { NuevapersonaComponent } from './components/personas/nuevapersona/nuevapersona.component';
import { BuildingsComponent } from './components/buildings/buildings.component';
import { EditBuildingComponent } from './components/buildings/edit-building/edit-building.component';
import { AgmCoreModule } from '@agm/core';
import { DoorsComponent } from './components/doors/doors.component';
import { WebcamModule } from 'ngx-webcam';
import { CameraComponent } from './components/camera/camera.component';
import { AssignmentsComponent } from './components/assignments/assignments.component';
import { NotificationsComponent } from './components/notifications/notifications.component';
import { IngresarComponent } from './components/ingresar/ingresar.component';
import { HomeComponent } from './components/home/home.component';
import { registerLocaleData } from '@angular/common';

import localeES from "@angular/common/locales/es";
import { ProductosComponent } from './components/productos/productos.component';
import { PreciosComponent } from './components/precios/precios.component';
import { FacturasComponent } from './components/facturas/facturas.component';
import { PagosComponent } from './components/pagos/pagos.component';
import { ListarFacturasComponent } from './components/listar-facturas/listar-facturas.component';
import { MostrarProductosPrecioComponent } from './components/mostrar-productos-precio/mostrar-productos-precio.component';
import { PagoVentanaComponent } from './components/pago-ventana/pago-ventana.component';
import { RealizarPagoComponent } from './components/realizar-pago/realizar-pago.component';
import { GenerarFacturaComponent } from './components/generar-factura/generar-factura.component';
registerLocaleData(localeES, "es");

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
    NuevapersonaComponent,
    BuildingsComponent,
    EditBuildingComponent,
    DoorsComponent,
    CameraComponent,
    AssignmentsComponent,
    NotificationsComponent,
    IngresarComponent,
    HomeComponent,
    ProductosComponent,
    PreciosComponent,
    FacturasComponent,
    PagosComponent,
    ListarFacturasComponent,
    MostrarProductosPrecioComponent,
    PagoVentanaComponent,
    RealizarPagoComponent,
    GenerarFacturaComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    NgbModule,
    ReactiveFormsModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyCA22WX7c4qIzJRKwnbvG8_2gqlSrMfk1E'
    }),
    WebcamModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: TenantInterceptor, multi: true },
    { provide: LOCALE_ID, useValue: "es" }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
