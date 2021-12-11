import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AssignmentsComponent } from './components/assignments/assignments.component';
import { BuildingsComponent } from './components/buildings/buildings.component';
import { EditBuildingComponent } from './components/buildings/edit-building/edit-building.component';
import { DashBoardComponent } from './components/dash-board/dash-board.component';
import { DoorsComponent } from './components/doors/doors.component';
import { FacturasComponent } from './components/facturas/facturas.component';
import { HomeComponent } from './components/home/home.component';
import { IngresarComponent } from './components/ingresar/ingresar.component';
import { InicioComponent } from './components/inicio/inicio.component';
import { InstitucionesComponent } from './components/instituciones/instituciones.component';
import { NuevainstitucionComponent } from './components/instituciones/nuevainstitucion/nuevainstitucion.component';
import { ListarFacturasComponent } from './components/listar-facturas/listar-facturas.component';
import { LoginComponent } from './components/login/login.component';
import { MostrarProductosPrecioComponent } from './components/mostrar-productos-precio/mostrar-productos-precio.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { PagoVentanaComponent } from './components/pago-ventana/pago-ventana.component';
import { PagosComponent } from './components/pagos/pagos.component';
import { PaypalComponent } from './components/paypal/paypal.component';
import { NuevapersonaComponent } from './components/personas/nuevapersona/nuevapersona.component';
import { PersonasComponent } from './components/personas/personas.component';
import { PreciosComponent } from './components/precios/precios.component';
import { ProductosComponent } from './components/productos/productos.component';
import { EditUsuarioComponent } from './components/usuarios/edit-usuario/edit-usuario.component';
import { UsuariosComponent } from './components/usuarios/usuarios.component';
import { IsLoggedInGuard } from './guards/is-logged-in/is-logged-in.guard';
import { TieneRolGuard } from './guards/tiene-rol/tiene-rol.guard';
import { PrecioDataType } from './models/PrecioDatatype';


const routes: Routes = [
  {
    path: "",
    component: HomeComponent,
    children: [
      { path: "", component: InicioComponent },

      { path: "iniciarSesion", component: LoginComponent },

      { path: "dashboard", component: DashBoardComponent, canActivate: [IsLoggedInGuard] },

      { path: "nuevainstitucion", component: NuevainstitucionComponent, canActivate: [IsLoggedInGuard, TieneRolGuard], data: { roles: ['SuperAdmin'] } },
      { path: "nuevainstitucion/:id", component: NuevainstitucionComponent, canActivate: [IsLoggedInGuard, TieneRolGuard], data: { roles: ['SuperAdmin'] } },
      { path: "instituciones", component: InstitucionesComponent, canActivate: [IsLoggedInGuard, TieneRolGuard], data: { roles: ['SuperAdmin'] } },

      { path: "usuarios", component: UsuariosComponent, canActivate: [IsLoggedInGuard, TieneRolGuard], data: { roles: ['SuperAdmin', 'Admin'] } },
      { path: "usuarios/nuevo", component: EditUsuarioComponent, canActivate: [IsLoggedInGuard, TieneRolGuard], data: { roles: ['SuperAdmin', 'Admin'] } },
      { path: "usuarios/editar/:id", component: EditUsuarioComponent, canActivate: [IsLoggedInGuard, TieneRolGuard], data: { roles: ['SuperAdmin', 'Admin'] } },

      { path: "edificios", component: BuildingsComponent, canActivate: [IsLoggedInGuard, TieneRolGuard], data: { roles: ['Admin'] } },
      { path: "edificios/nuevo", component: EditBuildingComponent, canActivate: [IsLoggedInGuard, TieneRolGuard], data: { roles: ['Admin'] } },
      { path: "edificios/editar/:id", component: EditBuildingComponent, canActivate: [IsLoggedInGuard, TieneRolGuard], data: { roles: ['Admin'] } },

      { path: "puertas/edificio/:id", component: DoorsComponent, canActivate: [IsLoggedInGuard, TieneRolGuard], data: { roles: ['Admin'] } },

      { path: "personas", component: PersonasComponent, canActivate: [IsLoggedInGuard, TieneRolGuard], data: { roles: ['Admin', 'Portero', 'Gestor'] } },
      { path: "personas/nuevo", component: NuevapersonaComponent, canActivate: [IsLoggedInGuard, TieneRolGuard], data: { roles: ['Admin', 'Portero', 'Gestor'] } },
      { path: "personas/editar/:id", component: NuevapersonaComponent, canActivate: [IsLoggedInGuard, TieneRolGuard], data: { roles: ['Admin', 'Portero', 'Gestor'] } },

      { path: "asignaciones", component: AssignmentsComponent, canActivate: [IsLoggedInGuard, TieneRolGuard], data: { roles: ['Portero'] } },

      {path:"facturas",component:FacturasComponent},

      {path: "productos",component:ProductosComponent},

      {path: "precios", component:PreciosComponent},

      {path:"pago-ventana/:id",component:PagoVentanaComponent},

      {path:"mostrar-productos",component:MostrarProductosPrecioComponent},

      {path: "listar-facturas",component:ListarFacturasComponent},

      {path: "paypal", component:PaypalComponent}  
    ]
  },
  
  {
    path: "ingresar",
    component: IngresarComponent,
    canActivate: [IsLoggedInGuard, TieneRolGuard], data: { roles: ['Portero'] }
  },

  { path: '**', component: PageNotFoundComponent },
]


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
