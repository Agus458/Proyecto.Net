import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InicioComponent } from './components/inicio/inicio.component';
import { NuevainstitucionComponent } from './components/instituciones/nuevainstitucion/nuevainstitucion.component';
import { LoginComponent } from './components/login/login.component';


const routes: Routes = [
  { path: "", component: InicioComponent },
  { path: "iniciarSesion", component: LoginComponent },
  { path: "nuevainstitucion", component: NuevainstitucionComponent },
  
]


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
