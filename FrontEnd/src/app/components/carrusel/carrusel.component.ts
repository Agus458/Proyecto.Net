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

  selectedNoveltie: NoveltiesDataType;
  @Input() novelties: NoveltiesDataType[];
  private lenght = environment.controlApiUrl.length;
  backurl = environment.controlApiUrl.substring(0, this.lenght - 1);

  constructor() { }

  ngOnInit(): void {

  }

  select(novelty: NoveltiesDataType){
    this.selectedNoveltie = novelty;
  }
}


