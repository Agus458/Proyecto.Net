import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ProductosService } from 'src/app/services/productos/productos.service';
import {ProductosDataType} from 'src/app/models/ProductosDataType';
import { ToastService } from 'src/app/services/toast/toast.service';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-productos',
  templateUrl: './productos.component.html',
  styleUrls: ['./productos.component.css']
})
export class ProductosComponent implements OnInit {
  ProductosForm:FormGroup;

  productos: ProductosDataType[]
  selectProductos: ProductosDataType;
  page=9;
  size: number;

  constructor(
    private ProductosService: ProductosService,
    private modalService:  NgbModal,
    private router: Router,
    private toastService:ToastService) { }

  ngOnInit(): void {
    
  }
  getProducts(skip: number, take: number) {
    this.ProductosService.getAll(skip, take).subscribe(
      ok => {
        this.productos = ok.collection;
        this.size = ok.size;
      },
      error => {
        console.log(error);
      }
    );
  }
  open(content: any, productos: ProductosDataType) {
    this.selectProductos = productos;
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }
  
  delete(id: string) {
    this.ProductosService.delete(id).subscribe(
      ok => {
        this.toastService.show("Success", "Productos eliminado");
        this.modalService.dismissAll();
        this.getProducts(0, 10);
      },
      error => {
        console.log(error);

        this.toastService.show("Error", "Algo salio mal");
      }
    );
  }
  onPageChange(pageNum: number): void {
    this.getProducts((pageNum - 1) * 10, 10);
  }
}
