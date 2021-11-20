import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UserDataType } from 'src/app/models/UserDataType';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';
import { UsuariosService } from 'src/app/services/usuarios/usuarios.service';

@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.css']
})
export class UsuariosComponent implements OnInit {

  users: UserDataType[];
  selectedUser: UserDataType;

  constructor(
    private UsuariosService: UsuariosService,
    private modalService: NgbModal,
    public AuthenticationService: AuthenticationService
  ) { }

  ngOnInit(): void {
    this.UsuariosService.getAll().subscribe(
      ok => {
        this.users = ok;
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

  delete(id: string){
    this.UsuariosService.delete(id).subscribe();
    window.location.reload();
  }

}
