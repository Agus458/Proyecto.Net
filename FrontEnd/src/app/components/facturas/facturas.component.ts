import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';
import { FacturaDataType } from 'src/app/models/FacturaDataType';
import { FacturasService } from 'src/app/services/facturas/facturas.service';
import { ToastService } from 'src/app/services/toast/toast.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
@Component({
  selector: 'app-facturas',
  templateUrl: './facturas.component.html',
  styleUrls: ['./facturas.component.css']
})
export class FacturasComponent implements OnInit {

  facturas: FacturaDataType[];
  FacturaForm: FormGroup;
  selectFacturas: FacturaDataType;
  page= 6;
  size: number;
  
  constructor(
    private FormBuilder: FormBuilder,
    private FacturasService: FacturasService,
    private modalService: NgbModal,
    private router: Router,
    private toastService:ToastService
    ){ }

  ngOnInit(): void {
    this.getFacturas(0, 10);
    
    
  }
 
  getFacturas(skip: number, take: number) {
    this.FacturasService.getAll(skip, take).subscribe(
      ok => {
        this.facturas= ok.collection;
        this.size = ok.size;
      },
      error => {
        console.log(error);
      }
    );
  }
  
  open(content: any, facturas: FacturaDataType) {
    this.selectFacturas = facturas;
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

  delete(id: string) {
    this.FacturasService.delete(id).subscribe(
      ok => {
        this.toastService.show("Success", "Factura Eliminada");
        this.modalService.dismissAll();
        this.getFacturas(0, 10);
      },
      error => {
        console.log(error);

        this.toastService.show("Error", "Algo salio mal");
      }
    );
  }
  onPageChange(pageNum: number): void {
    this.getFacturas((pageNum - 1) * 10, 10);
  }
  submit(){
    const {tenant, monto} = this.FacturaForm.value;
  }

}
