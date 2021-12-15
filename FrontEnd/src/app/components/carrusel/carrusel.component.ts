import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NoveltiesDataType } from 'src/app/models/NoveltiesDataType';
import { ToastService } from 'src/app/services/toast/toast.service';
import { NoveltiesService } from 'src/app/services/novelties.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-carrusel',
  templateUrl: './carrusel.component.html',
  styleUrls: ['./carrusel.component.css']
})
export class CarruselComponent implements OnInit {

  Novelties: NoveltiesDataType[];
  size: number;
  buildingId: string;
  @Input()selectedNoveltie: NoveltiesDataType;
  private lenght = environment.controlApiUrl.length;
  backurl = environment.controlApiUrl.substring(0,this.lenght-1);

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

  onPageChange(pageNum: number): void {
    this.getNovelties((pageNum - 1) * 10, 10);
  }
}


