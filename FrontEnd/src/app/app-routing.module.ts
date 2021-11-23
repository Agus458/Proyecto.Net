import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BuildingsComponent } from './components/buildings/buildings.component';
import { EditBuildingComponent } from './components/buildings/edit-building/edit-building.component';
import { DashBoardComponent } from './components/dash-board/dash-board.component';
import { InicioComponent } from './components/inicio/inicio.component';
import { InstitucionesComponent } from './components/instituciones/instituciones.component';
import { NuevainstitucionComponent } from './components/instituciones/nuevainstitucion/nuevainstitucion.component';
import { LoginComponent } from './components/login/login.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { NuevapersonaComponent } from './components/personas/nuevapersona/nuevapersona.component';
import { PersonasComponent } from './components/personas/personas.component';
import { EditUsuarioComponent } from './components/usuarios/edit-usuario/edit-usuario.component';
import { UsuariosComponent } from './components/usuarios/usuarios.component';
import { IsLoggedInGuard } from './guards/is-logged-in/is-logged-in.guard';
import { TieneRolGuard } from './guards/tiene-rol/tiene-rol.guard';


const routes: Routes = [
  { path: "", component: InicioComponent },
  
  { path: "iniciarSesion", component: LoginComponent },
  
  { path: "dashboard", component: DashBoardComponent, canActivate: [IsLoggedInGuard, TieneRolGuard], data: { roles: ['SuperAdmin', 'Admin'] } },

  { path: "nuevainstitucion", component: NuevainstitucionComponent, canActivate: [IsLoggedInGuard, TieneRolGuard], data: { roles: ['SuperAdmin'] } },
  { path: "nuevainstitucion/:id", component: NuevainstitucionComponent, canActivate: [IsLoggedInGuard, TieneRolGuard], data: { roles: ['SuperAdmin'] } },
  { path: "instituciones", component: InstitucionesComponent, canActivate: [IsLoggedInGuard, TieneRolGuard], data: { roles: ['SuperAdmin'] } },
  
  { path: "usuarios", component: UsuariosComponent, canActivate: [IsLoggedInGuard, TieneRolGuard], data: { roles: ['SuperAdmin', 'Admin'] } },
  { path: "usuarios/nuevo", component: EditUsuarioComponent, canActivate: [IsLoggedInGuard, TieneRolGuard], data: { roles: ['SuperAdmin', 'Admin'] } },
  { path: "usuarios/editar/:id", component: EditUsuarioComponent, canActivate: [IsLoggedInGuard, TieneRolGuard], data: { roles: ['SuperAdmin', 'Admin'] } },

  { path: "buildings", component: BuildingsComponent, canActivate: [IsLoggedInGuard, TieneRolGuard], data: { roles: ['Admin'] } },
  { path: "buildings/nuevo", component: EditBuildingComponent, canActivate: [IsLoggedInGuard, TieneRolGuard], data: { roles: ['Admin'] } },
  { path: "buildings/editar/:id", component: EditBuildingComponent, canActivate: [IsLoggedInGuard, TieneRolGuard], data: { roles: ['Admin'] } },

  { path: "personas", component: PersonasComponent, canActivate: [IsLoggedInGuard, TieneRolGuard], data: { roles: ['Admin'] } },
  { path: "presonas/nuevo", component: NuevapersonaComponent, canActivate: [IsLoggedInGuard, TieneRolGuard], data: { roles: ['Admin'] } },
  { path: "presonas/editar/:id", component: NuevapersonaComponent, canActivate: [IsLoggedInGuard, TieneRolGuard], data: { roles: ['Admin'] } },

  { path: '**', component: PageNotFoundComponent },
]


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
