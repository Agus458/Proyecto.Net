import { Component, OnInit } from '@angular/core';
import { EventDataType } from 'src/app/models/EventDataType';
import { NoveltiesDataType } from 'src/app/models/NoveltiesDataType';
import { TenantDataType } from 'src/app/models/TenantDataType';
import { EventosService } from 'src/app/services/eventos/eventos.service';
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
  events: EventDataType[];

  constructor(
    private NoveltiesService: NoveltiesService,
    private TenantsService: TenantsService,
    private EventosService: EventosService
  ) { }

  ngOnInit(): void {
    this.TenantsService.get().subscribe(
      ok => {
        this.tenants = ok;
      }
    )
  }

  onChangeTenant(id: any) {
    if (id) {
      this.NoveltiesService.getByTenant(0, 10, id).subscribe(
        ok => {
          this.novelties = ok.collection;
        }
      );

      this.EventosService.getByTenant(id).subscribe(
        ok => {
          this.events = ok;
        }
      );
    } else {
      this.events = [];
      this.novelties = [];
    }
  }
}
