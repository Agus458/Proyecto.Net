import { Component, OnInit } from '@angular/core';
import { NoveltiesDataType } from 'src/app/models/NoveltiesDataType';
import { TenantDataType } from 'src/app/models/TenantDataType';
import { NoveltiesService } from 'src/app/services/novelties.service';
import { TenantsService } from 'src/app/services/tenants/tenants.service';

@Component({
  selector: 'app-inicio',
  templateUrl: './inicio.component.html',
  styleUrls: ['./inicio.component.css']
})
export class InicioComponent implements OnInit {

  tenants: TenantDataType[];
  novelties: NoveltiesDataType[];

  constructor(
    private NoveltiesService: NoveltiesService,
    private TenantsService: TenantsService
  ) { }

  ngOnInit(): void {
    this.TenantsService.get().subscribe(
      ok => {
        this.tenants = ok;
      }
    )
  }

  onChangeTenant(id: any) {
    this.NoveltiesService.getByTenant(0, 10, id).subscribe(
      ok => {
        this.novelties = ok.collection;
      }
    )
  }
}
