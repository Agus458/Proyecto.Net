import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UserDataType } from 'src/app/models/UserDataType';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';
import { ToastService } from 'src/app/services/toast/toast.service';
import { UsuariosService } from 'src/app/services/usuarios/usuarios.service';

@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.css']
})
export class UsuariosComponent implements OnInit {

  users: UserDataType[];
  selectedUser: UserDataType;
  page = 1;
  size: number;

  constructor(
    private UsuariosService: UsuariosService,
    private modalService: NgbModal,
    public AuthenticationService: AuthenticationService,
    private router: Router,
    private toastService: ToastService,
  ) { }

  ngOnInit(): void {
    this.getUsuarios(0, 10);
  }

  getUsuarios(skip: number, take: number) {
    this.UsuariosService.getAll(skip, take).subscribe(
      ok => {
        this.users = ok.collection;
        this.size = ok.size;
      },
      error => {
        console.log(error);
      }
    );
  }

  open(content: any, user: UserDataType) {
    this.selectedUser = user;
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

  delete(id: string) {
    this.UsuariosService.delete(id).subscribe(
      ok => {
        this.toastService.show("Success", "Usuario eliminado");
        this.modalService.dismissAll();
        this.getUsuarios(0, 10);
      },
      error => {
        console.log(error);

        this.toastService.show("Error", error.error?.Message ?? "Algo salió mal");
      }
    );
  }

  onPageChange(pageNum: number): void {
    this.getUsuarios((pageNum - 1) * 10, 10);
  }

}
