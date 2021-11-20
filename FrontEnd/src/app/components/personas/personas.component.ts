import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PersonDataType } from 'src/app/models/PersonDataType';
import { UserDataType } from 'src/app/models/UserDataType';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';
import { PersonasService } from 'src/app/services/personas/personas.service';


@Component({
  selector: 'app-personas',
  templateUrl: './personas.component.html',
  styleUrls: ['./personas.component.css']
})
export class PersonasComponent implements OnInit {
  

  personas: PersonDataType[];
  selectedPerson: PersonDataType;

  constructor(private PersonasService: PersonasService,
    private modalService: NgbModal,
    public AuthenticationService: AuthenticationService
  ) { }

  ngOnInit(): void {
    this.PersonasService.getAll().subscribe(
      ok => {
        this.personas = ok.collection;
        console.log(ok);
        
      },
      error => {
        console.log(error);
      }
    );
  }

  open(content: any, persona: PersonDataType) {
    this.selectedPerson = persona;
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

  delete(id: string){
    this.PersonasService.delete(id).subscribe();
    window.location.reload();
  }

}

