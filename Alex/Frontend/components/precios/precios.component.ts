import { Component, NgModuleFactoryLoader, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ActivatedRoute, Router } from '@angular/router';
import { PreciosService } from 'src/app/services/precios/precios.service';
import { ToastService } from 'src/app/services/toast/toast.service';
import { PrecioDataType } from 'src/app/models/PrecioDatatype';
import { FormBuilder, FormGroup } from '@angular/forms';
import { FacturasService } from 'src/app/services/facturas/facturas.service';
import { FacturaDataType } from 'src/app/models/FacturaDataType';
import { ProductosDataType } from 'src/app/models/ProductosDataType';
import { ProductosService } from 'src/app/services/productos/productos.service';

@Component({
  selector: 'app-precios',
  templateUrl: './precios.component.html',
  styleUrls: ['./precios.component.css']
})
export class PreciosComponent implements OnInit {
  PreciosForm: FormGroup;

  precios: PrecioDataType[]
  productos: ProductosDataType[];
  selectPrecio: PrecioDataType;
  factura: FacturaDataType;
  page=8;
  size: number;
  Productoid:string;
  constructor(

    private FormBuilder:FormBuilder,
    private PreciosService: PreciosService,
    private ProductosService:ProductosService,
    private modalService:  NgbModal,
    private router: Router,
    private activate: ActivatedRoute,
    private toastService:ToastService,

  ) { }

  ngOnInit(): void {
    
   this.PreciosForm = this.FormBuilder.group({
      precio:['']
    });
  }
  getPagos(skip: number, take: number) {
    this.PreciosService.getAll(skip, take).subscribe(
      ok => {
        this.precios= ok.collection;
        this.size = ok.size;
      },
      error => {
        console.log(error);
      }
    );
  }


  
  submit()
  {
    this.PreciosService.create(this.PreciosForm.value).subscribe(
      ok=>{console.log("Producto Creado")},
      error=>console.log("Algo Salio Mal"));
  }
  open(content: any, precios: PrecioDataType) {
    this.selectPrecio = precios;
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

  delete(id: string) {
    this.PreciosService.delete(id).subscribe(
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


  
}
