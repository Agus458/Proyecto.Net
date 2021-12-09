import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FacturaDataType } from 'src/app/models/FacturaDataType';
import { FacturasService } from 'src/app/services/facturas/facturas.service';
import { ToastService } from 'src/app/services/toast/toast.service';

@Component({
  selector: 'app-listar-facturas',
  templateUrl: './listar-facturas.component.html',
  styleUrls: ['./listar-facturas.component.css']
})
export class ListarFacturasComponent implements OnInit {

  facturas: FacturaDataType[];
  selectFactura: FacturaDataType;
  page = 11;
  size: number;
  constructor(
   
    private FacturaService: FacturasService,
    private modalService: NgbModal,
    private tostService: ToastService,
  ) { }

  ngOnInit(): void {
    this.getFacturas(0,10);
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
  
  open(content: any, facturas: FacturaDataType) {
    this.selectFactura = facturas;
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

  onPageChange(pageNum: number): void {
    this.getFacturas((pageNum - 1) * 10, 10);
  }


}
