import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FacturaDataType } from 'src/app/models/FacturaDataType';
import { FacturasService } from 'src/app/services/facturas/facturas.service';
import { PreciosService } from 'src/app/services/precios/precios.service';
import { PrecioDataType } from 'src/app/models/PrecioDatatype';
import { PagoDataType } from 'src/app/models/PagoDataType';
import { PagosService } from 'src/app/services/pagos/pagos.service';
import { ToastService } from 'src/app/services/toast/toast.service';

import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-pago-ventana',
  templateUrl: './pago-ventana.component.html',
  styleUrls: ['./pago-ventana.component.css']
})
export class PagoVentanaComponent implements OnInit {

  facturaId:string;
  facturas: FacturaDataType[];
  facturaselect: FacturaDataType;
  FacturaForm: FormGroup;
  
  page=11;
  size:number;
  constructor(
    private FormBuilder: FormBuilder,
    private PreciosService :PreciosService,
    private FacturaService :FacturasService,
    private modalService: NgbModal,
    private tostService: ToastService,
  ) { }

  ngOnInit(): void {
    
  }
  Pagar()
  {
   this.FacturaService.Pagar(this.facturaselect.id).subscribe(
     ok=>{console.log("Producto Creado")},
     error=>console.log("Algo Salio Mal"));
  }
/*
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
    
  }*/
}
