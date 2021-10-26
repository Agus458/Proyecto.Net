import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Location } from '@angular/common';
import { UsuariosService } from 'src/app/services/usuarios/usuarios.service';
import { ToastService } from 'src/app/services/toast/toast.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-edit-usuario',
  templateUrl: './edit-usuario.component.html',
  styleUrls: ['./edit-usuario.component.css']
})
export class EditUsuarioComponent implements OnInit {

  usuarioForm: FormGroup;

  constructor(
    private UsuariosService: UsuariosService,
    private FormBuilder: FormBuilder,
    public location: Location,
    private router: Router,
    private toastService: ToastService
  ) { }

  async ngOnInit() {
    this.usuarioForm = this.FormBuilder.group({
      email: ["", [Validators.required, Validators.email]],
      password: ["", [Validators.required]],
      role: ["", [Validators.required]]
    });
  }

  submit() {
    this.UsuariosService.create(this.usuarioForm.value).subscribe(
      ok => {
        this.toastService.show("Success", "Usuario creado");
        this.router.navigateByUrl("/usuarios");
      },
      error => {
        console.log(error);

        this.toastService.show("Error", "Algo salio mal");
      }
    );
  }
}
