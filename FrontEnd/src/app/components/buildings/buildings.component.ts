import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BuildingDataType } from 'src/app/models/BuildingDataType';
import { BuildingsService } from 'src/app/services/building/buildings.service';
import { ToastService } from 'src/app/services/toast/toast.service';

@Component({
  selector: 'app-buildings',
  templateUrl: './buildings.component.html',
  styleUrls: ['./buildings.component.css']
})
export class BuildingsComponent implements OnInit {

  buildings: BuildingDataType[];
  selectedBuilding: BuildingDataType;
  page = 1;
  size: number;

  constructor(
    private BuildingsService: BuildingsService,
    private modalService: NgbModal,
    private router: Router,
    private toastService: ToastService,
  ) { }

  ngOnInit(): void {
    this.getBuildings(0, 10);
  }

  getBuildings(skip: number, take: number) {
    this.BuildingsService.getAll(skip, take).subscribe(
      ok => {
        this.buildings = ok.collection;
        this.size = ok.size;
      },
      error => {
        console.log(error);
      }
    );
  }

  open(content: any, building: BuildingDataType) {
    this.selectedBuilding = building;
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

  delete(id: string) {
    this.BuildingsService.delete(id).subscribe(
      ok => {
        this.toastService.show("Success", "Edificio eliminado");
        this.modalService.dismissAll();
        this.getBuildings(0, 10);
      },
      error => {
        console.log(error);

        this.toastService.show("Error", "Algo salio mal");
      }
    );
  }
  onPageChange(pageNum: number): void {
    this.getBuildings((pageNum - 1) * 10, 10);
  }

}
