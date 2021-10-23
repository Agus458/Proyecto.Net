import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { TenantDataType } from 'src/app/models/TenantDataType';
import { TenantsService } from 'src/app/services/tenants/tenants.service';

@Component({
  selector: 'app-nuevainstitucion',
  templateUrl: './nuevainstitucion.component.html',
  styleUrls: ['./nuevainstitucion.component.css']
})
export class NuevainstitucionComponent implements OnInit {

  tenant: TenantDataType;

  institucionForm: FormGroup;

  constructor(
    private route: ActivatedRoute,
    private TenantService: TenantsService,
    private FormBuilder: FormBuilder
  ) { }

  async ngOnInit() {
    const routeParams = this.route.snapshot.paramMap;
    const IdFromRoute = routeParams.get('id');

    this.institucionForm = this.FormBuilder.group({
      rut: ["", [Validators.required]],
      socialReason: ["", [Validators.required]],
      email: ["", [Validators.required, Validators.email]],
      password: ["", [Validators.required]]
    });

    if (IdFromRoute) {
      try {
        this.tenant = await this.TenantService.getById(IdFromRoute).toPromise();

        if (this.tenant) {
          this.institucionForm.removeControl("password");
          this.institucionForm.removeControl("email");
        }

        this.institucionForm.patchValue(this.tenant);
      } catch (error) {
        console.log(error);
      }
    }
  }

  submit() {
    if (!this.tenant) {
      this.TenantService.create(this.institucionForm.value).subscribe(
        ok => {
          console.log(ok);
        },
        error => {
          console.log(error);
        }
      );
    } else {
      this.TenantService.update(this.tenant.id, this.institucionForm.value).subscribe(
        ok => {
          console.log(ok);
        },
        error => {
          console.log(error);
        }
      );
    }

    window.location.reload();
  }
}
