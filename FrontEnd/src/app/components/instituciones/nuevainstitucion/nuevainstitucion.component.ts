import { Component, OnInit } from '@angular/core';
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

  constructor(
    private route: ActivatedRoute,
    private TenantService: TenantsService
  ) { }

  ngOnInit(): void {
    const routeParams = this.route.snapshot.paramMap;
    const IdFromRoute = routeParams.get('id');

    if (IdFromRoute) {
      this.TenantService.getById(IdFromRoute).subscribe(
        ok => {
          this.tenant = ok;
        },
        error => {
          console.log(error);
        }
      );
    }
  }

}
