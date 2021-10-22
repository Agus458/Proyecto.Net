import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TenantDataType } from 'src/app/models/TenantDataType';
import { TenantsService } from 'src/app/services/tenants/tenants.service';

@Component({
  selector: 'app-instituciones',
  templateUrl: './instituciones.component.html',
  styleUrls: ['./instituciones.component.css']
})
export class InstitucionesComponent implements OnInit {

  tenants: TenantDataType[];
  selectedTenant: TenantDataType;

  constructor(
    private TenantsService: TenantsService,
    private modalService: NgbModal
  ) { }

  ngOnInit(): void {
    this.TenantsService.getAll().subscribe(
      ok => {
        this.tenants = ok;
      },
      error => {
        console.log(error);
      }
    );
  }

  open(content: any, tenant: TenantDataType) {
    this.selectedTenant = tenant;
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }
}
