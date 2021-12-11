import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { DoorDataType } from 'src/app/models/DoorDataType';
import { AsignacionesService } from 'src/app/services/asignaciones/asignaciones.service';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';

@Component({
  selector: 'app-assignment-dialog',
  templateUrl: './assignment-dialog.component.html',
  styleUrls: ['./assignment-dialog.component.css']
})
export class AssignmentDialogComponent implements OnInit {

  @Output() closeEvent: EventEmitter<void> = new EventEmitter<void>();
  @Output() reloadEvent: EventEmitter<void> = new EventEmitter<void>();

  page = 1;
  size: number;
  doors: DoorDataType[] = [];

  constructor(
    private AsignacionesService: AsignacionesService,
    public AuthenticationService: AuthenticationService
  ) { }

  ngOnInit(): void {
    this.getDoors(0, 10);
  }

  close() {
    this.closeEvent.emit();
  }

  getDoors(skip: number, take: number) {
    this.AsignacionesService.getDoors(skip, take).subscribe(
      ok => {
        this.doors = ok.collection;
        this.size = ok.size;
      }
    );
  }

  onPageChange(pageNum: number): void {
    this.getDoors((pageNum - 1) * 10, 10);
  }

  seleccionar(id: string) {
    this.AsignacionesService.create(id).subscribe(
      ok => {
        this.reloadEvent.emit();
        this.getDoors(0, 10);
      },
      error => {
        console.log(error);
      }
    );
  }
}
