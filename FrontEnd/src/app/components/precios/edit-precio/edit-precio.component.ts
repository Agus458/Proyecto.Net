import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PreciosService } from 'src/app/services/precios/precios.service';
import { ToastService } from 'src/app/services/toast/toast.service';

@Component({
  selector: 'app-edit-precio',
  templateUrl: './edit-precio.component.html',
  styleUrls: ['./edit-precio.component.css']
})
export class EditPrecioComponent implements OnInit {

  PrecioForm: FormGroup;
  productId: string;

  constructor(
    private PreciosService: PreciosService,
    private FormBuilder: FormBuilder,
    public location: Location,
    private route: ActivatedRoute,
    private router: Router,
    private toastService: ToastService
  ) { }

  async ngOnInit() {
    const routeParams = this.route.snapshot.paramMap;
    let IdFromRoute = routeParams.get('id');

    if (IdFromRoute) {
      this.productId = IdFromRoute;
    }
    
    this.PrecioForm = this.FormBuilder.group({
      amount: ["", [Validators.required]],
      validDate: ["", [Validators.required]],
      productId: [this.productId, [Validators.required]]
    });

    IdFromRoute = routeParams.get('precioId');

    if (IdFromRoute) {
      this.PreciosService.getById(IdFromRoute).subscribe(
        ok => {
          this.PrecioForm.addControl("id", new FormControl('', [Validators.required]));
          this.PrecioForm.patchValue(ok)
        }
      );
    }
  }

  submit() {
    if (this.PrecioForm.contains("id")) {
      const id = this.PrecioForm.controls.id.value;

      this.PreciosService.update(id, this.PrecioForm.value).subscribe(
        ok => {
          this.toastService.show("Success", "Precio actualizado");
          this.router.navigateByUrl("/productos");
        },
        error => this.toastService.show("Error", error.error?.Message ?? "Algo salió mal")
      );
    } else {
      this.PreciosService.create(this.PrecioForm.value).subscribe(
        ok => {
          this.toastService.show("Success", "Precio creado");
          this.router.navigateByUrl("/productos");
        },
        error => {
          console.log(error);

          this.toastService.show("Error", error.error?.Message ?? "Algo salió mal");
        }
      );
    }
  }

}
