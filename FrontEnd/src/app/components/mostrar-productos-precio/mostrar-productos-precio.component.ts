import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ProductosDataType } from 'src/app/models/ProductosDataType';
import { ProductosService } from 'src/app/services/productos/productos.service';
import { ToastService } from 'src/app/services/toast/toast.service';


@Component({
  selector: 'app-mostrar-productos-precio',
  templateUrl: './mostrar-productos-precio.component.html',
  styleUrls: ['./mostrar-productos-precio.component.css']
})
export class MostrarProductosPrecioComponent implements OnInit {

  productos: ProductosDataType[];
  selectProducto: ProductosDataType;
  page = 12;
  size: number;
  constructor(
   
    private ProductosService: ProductosService,
    private modalService: NgbModal,
    private tostService: ToastService,
  ) { }
  ngOnInit(): void {
    this.getProductos(0,10);
  }
  getProductos(skip: number, take: number) {
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
  
  onPageChange(pageNum: number): void {
    this.getProductos((pageNum - 1) * 10, 10);
  }

}
