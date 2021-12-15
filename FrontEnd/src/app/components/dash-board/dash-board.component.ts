import { Component, OnInit } from '@angular/core';
import { TenantDataType } from 'src/app/models/TenantDataType';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';
import { TenantsService } from 'src/app/services/tenants/tenants.service';

@Component({
  selector: 'app-dash-board',
  templateUrl: './dash-board.component.html',
  styleUrls: ['./dash-board.component.css']
})
export class DashBoardComponent implements OnInit {

  tenants: TenantDataType[] = [];
  selected: string = ""

  constructor(
    public AuthenticationService: AuthenticationService,
    private TenantService: TenantsService
  ) { }

  ngOnInit(): void {
    if (this.AuthenticationService.hasRole('SuperAdmin')) {
      this.TenantService.get().subscribe(
        ok => this.tenants = ok
      );
    }

    var tenant = this.AuthenticationService.getTenant()
    if (tenant) {
      this.selected = tenant;
    }
  }

  onChangeTenant(id: any) {
    localStorage.setItem("tenant", JSON.stringify(id));

    var Tenant = this.tenants.find(elem => elem.id == id);

    localStorage.setItem("socialReason", JSON.stringify(Tenant ? Tenant.socialReason : ""));
  }

}
