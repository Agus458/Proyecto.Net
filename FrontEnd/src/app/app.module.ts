import { LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { LoginComponent } from './components/login/login.component';
import { NgbDateAdapter, NgbDateParserFormatter, NgbModule, NgbTimeAdapter } from '@ng-bootstrap/ng-bootstrap';
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
import { EventosComponent } from './components/eventos/eventos.component';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/moment';
import moment from 'moment-timezone';
import { CalendarComponent } from './components/calendar/calendar.component';
import { EditEventoComponent } from './components/eventos/edit-evento/edit-evento.component';
import { CustomDateParserFormatter, DateAdapterService } from './services/date-adapter/date-adapter.service';
import { TimeAdapterService } from './services/time-adapter/time-adapter.service';
import { IngresosComponent } from './components/ingresos/ingresos.component';
import { EditIngresoComponent } from './components/ingresos/edit-ingreso/edit-ingreso.component';
import { EditDoorComponent } from './components/doors/edit-door/edit-door.component';

export function momentAdapterFactory() {
  return adapterFactory(moment);
}

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
    EventosComponent,
    CalendarComponent,
    EditEventoComponent,
    IngresosComponent,
    EditIngresoComponent,
    EditDoorComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    NgbModule,
    ReactiveFormsModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyCA22WX7c4qIzJRKwnbvG8_2gqlSrMfk1E'
    }),
    WebcamModule,
    CalendarModule.forRoot({ provide: DateAdapter, useFactory: momentAdapterFactory }),
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: TenantInterceptor, multi: true },
    { provide: LOCALE_ID, useValue: "es" },
    { provide: NgbDateAdapter, useClass: DateAdapterService },
    { provide: NgbDateParserFormatter, useClass: CustomDateParserFormatter },
    { provide: NgbTimeAdapter, useClass: TimeAdapterService }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
