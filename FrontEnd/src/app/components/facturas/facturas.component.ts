import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FacturaDataType } from 'src/app/models/FacturaDataType';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';
import { FacturasService } from 'src/app/services/facturas/facturas.service';
import { PagosService } from 'src/app/services/pagos/pagos.service';

@Component({
  selector: 'app-facturas',
  templateUrl: './facturas.component.html',
  styleUrls: ['./facturas.component.css']
})
export class FacturasComponent implements OnInit {

  facturas: FacturaDataType[];
  page = 1;
  size: number;
  selectedBill: FacturaDataType;

  constructor(
    private FacturasService: FacturasService,
    private PagosService: PagosService,
    private modalService: NgbModal,
    public AuthenticationService: AuthenticationService,
  ) { }

  ngOnInit(): void {
    this.getFacturas(0, 10);
  }

  getFacturas(skip: number, take: number) {
    this.FacturasService.getAll(skip, take).subscribe(
      ok => {
        this.facturas = ok.collection;
        this.size = ok.size;
      },
      error => {
        console.log(error);
      }
    );
  }

  onPageChange(pageNum: number): void {
    this.getFacturas((pageNum - 1) * 10, 10);
  }

  open(content: any, bill: FacturaDataType) {
    this.selectedBill = bill;
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

  pay(facturaId: string){
    this.PagosService.pay(facturaId).subscribe(
      ok => {
        console.log(ok);
        this.modalService.dismissAll();
      }
    )
  }
}
