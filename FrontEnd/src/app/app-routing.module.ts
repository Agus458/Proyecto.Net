import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashBoardComponent } from './components/dash-board/dash-board.component';
import { InicioComponent } from './components/inicio/inicio.component';
import { InstitucionesComponent } from './components/instituciones/instituciones.component';
import { NuevainstitucionComponent } from './components/instituciones/nuevainstitucion/nuevainstitucion.component';
import { LoginComponent } from './components/login/login.component';
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
]


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
