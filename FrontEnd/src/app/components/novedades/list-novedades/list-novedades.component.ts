import { Component, OnInit } from '@angular/core';
import { NoveltiesService } from 'src/app/services/novelties.service';
import { NoveltiesDataType } from 'src/app/models/NoveltiesDataType';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from 'src/app/services/toast/toast.service';

@Component({
  selector: 'app-list-novedades',
  templateUrl: './list-novedades.component.html',
  styleUrls: ['./list-novedades.component.css']
})
export class ListNovedadesComponent implements OnInit {

  Novelties: NoveltiesDataType[];
  selectedNoveltie: NoveltiesDataType;
  page = 1;
  size: number;
  buildingId: string;

  constructor(
    private NoveltiesService: NoveltiesService,
    private modalService: NgbModal,
    private route: ActivatedRoute,
    private toastService: ToastService,
  ) { }

  ngOnInit(): void {
    const routeParams = this.route.snapshot.paramMap;
    const IdFromRoute = routeParams.get('id');

    if (IdFromRoute) {
      this.buildingId = IdFromRoute;

      this.getNovelties(0, 10);
    }
  }

  getNovelties(skip: number, take: number) {
    this.NoveltiesService.getAll(skip, take, this.buildingId).subscribe(
      ok => {
        this.Novelties = ok.collection;
        this.size = ok.size;
      }
    );
  }

  open(content: any, Novelties: NoveltiesDataType) {
    this.selectedNoveltie = Novelties;
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

  delete(id: string) {
    this.NoveltiesService.delete(id, this.buildingId).subscribe(
      ok => {
        this.toastService.show("Success", "Edificio eliminado");
        this.modalService.dismissAll();
        this.getNovelties(0, 10);
      },
      error => {
        console.log(error);

        this.toastService.show("Error", error.error?.Message ?? "Algo salió mal");
      }
    );
  }

  onPageChange(pageNum: number): void {
    this.getNovelties((pageNum - 1) * 10, 10);
  }


}

