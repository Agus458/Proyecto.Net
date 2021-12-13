import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SalonDataType } from 'src/app/models/SalonDataType';
import { SalonesService } from 'src/app/services/salones/salones.service';
import { ToastService } from 'src/app/services/toast/toast.service';

@Component({
  selector: 'app-salones',
  templateUrl: './salones.component.html',
  styleUrls: ['./salones.component.css']
})
export class SalonesComponent implements OnInit {

  salones: SalonDataType[];
  selectedDoor: SalonDataType;
  page = 1;
  size: number;
  buildingId: string;

  constructor(
    private SalonesService: SalonesService,
    private toastService: ToastService,
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    const routeParams = this.route.snapshot.paramMap;
    const IdFromRoute = routeParams.get('id');

    if (IdFromRoute) {
      this.buildingId = IdFromRoute;
      this.getSalones(0, 10);
    }
  }

  getSalones(skip: number, take: number) {
    this.SalonesService.getAll(skip, take, this.buildingId).subscribe(
      ok => {
        this.salones = ok.collection;
        this.size = ok.size;
      },
      error => {
        console.log(error);
      }
    );
  }

  delete(id: string) {
    this.SalonesService.delete(id).subscribe(
      ok => {
        this.toastService.show("Success", "Edificio eliminado");
        this.getSalones(0, 10);
      },
      error => {
        console.log(error);
        this.toastService.show("Error", "Algo salio mal");
      }
    );
  }

  onPageChange(pageNum: number): void {
    this.getSalones((pageNum - 1) * 10, 10);
  }

}
