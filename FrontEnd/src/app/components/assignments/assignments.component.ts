import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AssignmentDataType } from 'src/app/models/AssignmentDataType';
import { EntryDataType } from 'src/app/models/EntryDataType';
import { AsignacionesService } from 'src/app/services/asignaciones/asignaciones.service';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';
import { IngresosService } from 'src/app/services/ingresos/ingresos.service';

@Component({
  selector: 'app-assignments',
  templateUrl: './assignments.component.html',
  styleUrls: ['./assignments.component.css']
})
export class AssignmentsComponent implements OnInit {

  page = 1;
  size: number;
  assignments: AssignmentDataType[] = [];

  constructor(
    private AsignacionesService: AsignacionesService,
    public AuthenticationService: AuthenticationService,
    private modalService: NgbModal
  ) { }

  ngOnInit(): void {
    this.getIngresos(0, 10);
  }

  getIngresos(skip: number, take: number) {
    this.AsignacionesService.getAll(skip, take).subscribe(
      ok => {
        this.assignments = ok.collection;
        this.size = ok.size;
      }
    );
  }

  onPageChange(pageNum: number): void {
    this.getIngresos((pageNum - 1) * 10, 10);
  }

  delete(id: string){
    this.AsignacionesService.delete(id).subscribe(
      ok => {
        this.getIngresos(0, 10);
      }
    );
  }

  open(content: any) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

}
