import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ProductosService } from 'src/app/services/productos/productos.service';
import { ProductosDataType } from 'src/app/models/ProductosDataType';
import { ToastService } from 'src/app/services/toast/toast.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PreciosService } from 'src/app/services/precios/precios.service';
import { PrecioDataType } from 'src/app/models/PrecioDatatype';

@Component({
  selector: 'app-productos',
  templateUrl: './productos.component.html',
  styleUrls: ['./productos.component.css']
})
export class ProductosComponent implements OnInit {

  productos: ProductosDataType[];
  page = 1;
  size: number;

  constructor(
    private ProductosService: ProductosService,
    private router: Router,
    private toastService: ToastService) { }

  ngOnInit(): void {
    this.getProducts(0, 10)
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

  delete(id: string) {
    this.ProductosService.delete(id).subscribe(
      ok => {
        this.toastService.show("Success", "Producto eliminado");
        this.getProducts(0, 10);
      },
      error => {
        console.log(error);

        this.toastService.show("Error", error.error?.Message ?? "Algo salió mal");
      }
    );
  }

  onPageChange(pageNum: number): void {
    this.getProducts((pageNum - 1) * 10, 10);
  }

}
