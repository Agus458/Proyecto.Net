import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Location } from '@angular/common';
import { UsuariosService } from 'src/app/services/usuarios/usuarios.service';
import { ToastService } from 'src/app/services/toast/toast.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';
import { BuildingDataType } from 'src/app/models/BuildingDataType';
import { BuildingsService } from 'src/app/services/building/buildings.service';

@Component({
  selector: 'app-edit-usuario',
  templateUrl: './edit-usuario.component.html',
  styleUrls: ['./edit-usuario.component.css']
})
export class EditUsuarioComponent implements OnInit {

  usuarioForm: FormGroup;
  editing = false;
  buildings: BuildingDataType[];

  constructor(
    private UsuariosService: UsuariosService,
    private BuildingsService: BuildingsService,
    private FormBuilder: FormBuilder,
    public location: Location,
    private route: ActivatedRoute,
    private router: Router,
    private toastService: ToastService,
    public AuthenticationService: AuthenticationService
  ) { }

  async ngOnInit() {
    this.usuarioForm = this.FormBuilder.group({
      email: ["", [Validators.required, Validators.email]],
      password: ["", [Validators.required]],
      name: ["", [Validators.required]],
      lastName: ["", [Validators.required]],
      role: ["", [Validators.required]]
    });

    const routeParams = this.route.snapshot.paramMap;
    const IdFromRoute = routeParams.get('id');

    if (IdFromRoute) {
      this.UsuariosService.getById(IdFromRoute).subscribe(
        ok => {
          this.usuarioForm.addControl("id", new FormControl('', [Validators.required]));
          this.usuarioForm.patchValue(ok);
          this.editing = true;
          this.usuarioForm.removeControl("email");
          this.usuarioForm.removeControl("password");
          this.usuarioForm.removeControl("role");
        }
      );
    }

  }

  submit() {
    console.log(this.usuarioForm.value);

    if (this.usuarioForm.contains("id")) {
      const id = this.usuarioForm.controls.id.value;
      this.UsuariosService.update(id, this.usuarioForm.value).subscribe(
        ok => {
          this.toastService.show("Success", "Usuario actualizado");
          this.router.navigateByUrl("/usuarios");
        },
        error => {
          this.toastService.show("Error", error.error?.Message ?? "Algo salió mal");
        }
      );
    } else {
      this.UsuariosService.create(this.usuarioForm.value).subscribe(
        ok => {
          this.toastService.show("Success", "Usuario creado");
          this.router.navigateByUrl("/usuarios");
        },
        error => {
          this.toastService.show("Error", error.error?.Message ?? "Algo salió mal");
        }
      );
    }
  }

  onChangeRole(){
    if(this.usuarioForm.controls.role.value == "Portero"){
      this.BuildingsService.getList().subscribe(
        ok => {
          this.buildings = ok;
          console.log(ok);
          
        }
      )
      this.usuarioForm.addControl("buildingId", new FormControl("", [Validators.required]));
    } else {
      this.buildings = [];
      this.usuarioForm.removeControl("buildingId");
    }
  }
}
