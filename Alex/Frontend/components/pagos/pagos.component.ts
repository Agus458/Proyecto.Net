import { Component, NgModuleFactoryLoader, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';
import { PagosService } from 'src/app/services/pagos/pagos.service';
import { ToastService } from 'src/app/services/toast/toast.service';
import { PagoDataType } from 'src/app/models/PagoDataType';


@Component({
  selector: 'app-pagos',
  templateUrl: './pagos.component.html',
  styleUrls: ['./pagos.component.css']
})
export class PagosComponent implements OnInit {
  
  pagos: PagoDataType[]
  selectPago: PagoDataType;
  page=7;
  size: number;

  constructor(
    private PagoService: PagosService,
    private modalService:  NgbModal,
    private router: Router,
    private toastService:ToastService
  ){}
  ngOnInit(): void {
    this.getPagos(0, 10);
  }
  getPagos(skip: number, take: number) {
    this.PagoService.getAll(skip, take).subscribe(
      ok => {
        this.pagos= ok.collection;
        this.size = ok.size;
      },
      error => {
        console.log(error);
      }
    );
  }
  
  open(content: any, pagos: PagoDataType) {
    this.selectPago = pagos;
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

  delete(id: string) {
    this.PagoService.delete(id).subscribe(
      ok => {
        this.toastService.show("Success", "Edificio eliminado");
        this.modalService.dismissAll();
        this.getPagos(0, 10);
      },
      error => {
        console.log(error);

        this.toastService.show("Error", "Algo salio mal");
      }
    );
  }
  onPageChange(pageNum: number): void {
    this.getPagos((pageNum - 1) * 10, 10);
  }

}
