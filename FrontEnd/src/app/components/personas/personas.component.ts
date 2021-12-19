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

  page = 1;
  size: number;
  personas: PersonDataType[];
  selectedPerson: PersonDataType;

  constructor(private PersonasService: PersonasService,
    private modalService: NgbModal,
    public AuthenticationService: AuthenticationService
  ) { }

  ngOnInit(): void {
    this.getPersons(0, 10);
  }

  open(content: any, persona: PersonDataType) {
    this.selectedPerson = persona;
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

  getPersons(skip: number, take: number) {
    this.PersonasService.getAll(skip, take).subscribe(
      ok => {
        this.personas = ok.collection;
        this.size = ok.size;
      },
      error => {
        console.log(error);
      }
    );
  }

  onPageChange(pageNum: number): void {
    this.getPersons((pageNum - 1) * 10, 10);
  }

  delete(id: string) {
    this.PersonasService.delete(id).subscribe(
      ok => {
        this.getPersons(0, 10);
        this.modalService.dismissAll();
      }
    );
  }

  fileSelect(event: any) {
    const file: File = event.target.files[0];

    if (file) {
      this.PersonasService.csv(file).subscribe(
        ok => {
          this.getPersons(0, 10);
        }
      );
    }
  }
}

