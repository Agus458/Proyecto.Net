import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TenantDataType } from 'src/app/models/TenantDataType';
import { TenantsService } from 'src/app/services/tenants/tenants.service';
import { Location } from '@angular/common';

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
    private router: Router,
    private TenantService: TenantsService,
    private FormBuilder: FormBuilder,
    public location: Location
  ) { }

  async ngOnInit() {
    const routeParams = this.route.snapshot.paramMap;
    const IdFromRoute = routeParams.get('id');

    this.institucionForm = this.FormBuilder.group({
      rut: ["", [Validators.required]],
      socialReason: ["", [Validators.required]]
    });

    if (IdFromRoute) {
      try {
        this.tenant = await this.TenantService.getById(IdFromRoute).toPromise();

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
          this.router.navigateByUrl("/instituciones")
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
  }
}
