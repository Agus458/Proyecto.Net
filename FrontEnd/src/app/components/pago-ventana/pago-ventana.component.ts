import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FacturaDataType } from 'src/app/models/FacturaDataType';
import { FacturasService } from 'src/app/services/facturas/facturas.service';
import { ToastService } from 'src/app/services/toast/toast.service';

@Component({
  selector: 'app-pago-ventana',
  templateUrl: './pago-ventana.component.html',
  styleUrls: ['./pago-ventana.component.css']
})
export class PagoVentanaComponent implements OnInit {

  facturas: FacturaDataType[];
  selectFactura: FacturaDataType;
  page=11;
  size:number;
  constructor(
    private FacturaService:FacturasService,
    private modalService: NgbModal,
    private tostService: ToastService,
  ) { }

  ngOnInit(): void {
  }
  getFacturas(skip: number, take: number) {
    this.FacturaService.getAll(skip, take).subscribe(
      ok => {
        this.facturas = ok.collection;
        this.size = ok.size;
      },
      error => {
        console.log(error);
      }
    );
    
  }
}
