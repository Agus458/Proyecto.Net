import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductosService } from 'src/app/services/productos/productos.service';
import { ToastService } from 'src/app/services/toast/toast.service';

@Component({
  selector: 'app-edit-producto',
  templateUrl: './edit-producto.component.html',
  styleUrls: ['./edit-producto.component.css']
})
export class EditProductoComponent implements OnInit {

  ProductForm: FormGroup;

  constructor(
    private ProductosService: ProductosService,
    private FormBuilder: FormBuilder,
    public location: Location,
    private route: ActivatedRoute,
    private router: Router,
    private toastService: ToastService
  ) { }

  async ngOnInit() {
    this.ProductForm = this.FormBuilder.group({
      name: ["", [Validators.required]],
      cantBuildings: ["", [Validators.required]],
      cantRooms: ["", [Validators.required]],
    });

    const routeParams = this.route.snapshot.paramMap;
    let IdFromRoute = routeParams.get('id');

    if (IdFromRoute) {
      this.ProductosService.getById(IdFromRoute).subscribe(
        ok => {
          this.ProductForm.addControl("id", new FormControl('', [Validators.required]));
          this.ProductForm.patchValue(ok)
        }
      );
    }
  }

  submit() {
    if (this.ProductForm.contains("id")) {
      const id = this.ProductForm.controls.id.value;
      this.ProductosService.update(id, this.ProductForm.value).subscribe(
        ok => {
          this.toastService.show("Success", "Producto actualizado");
          this.router.navigateByUrl("/productos");
        },
        err => this.toastService.show("Error", "Algo salio mal")
      );
    } else {
      this.ProductosService.create(this.ProductForm.value).subscribe(
        ok => {
          this.toastService.show("Success", "Producto creado");
          this.router.navigateByUrl("/productos");
        },
        error => {
          console.log(error);

          this.toastService.show("Error", "Algo salio mal");
        }
      );
    }
  }

}
