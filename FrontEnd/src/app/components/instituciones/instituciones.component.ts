import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TenantDataType } from 'src/app/models/TenantDataType';
import { TenantsService } from 'src/app/services/tenants/tenants.service';

const FILTER_PAG_REGEX = /[^0-9]/g;

@Component({
  selector: 'app-instituciones',
  templateUrl: './instituciones.component.html',
  styleUrls: ['./instituciones.component.css']
})
export class InstitucionesComponent implements OnInit {

  tenants: TenantDataType[];
  selectedTenant: TenantDataType;
  page = 1;
  size: number;

  constructor(
    private TenantsService: TenantsService,
    private modalService: NgbModal
  ) { }

  ngOnInit(): void {
    this.getTenants(0,10);
  }

  getTenants(skip: number, take: number){
    this.TenantsService.getAll(skip, take).subscribe(
      ok => {
        this.tenants = ok.collection;
        this.size = ok.size;
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

  delete(id: string) {
    this.TenantsService.delete(id).subscribe(
      ok => {
        console.log("eliminado");
      },
      error => {
        console.log(error);
      }
    );
    this.getTenants(0,10);
  }

  onPageChange(pageNum: number): void {
    this.getTenants((pageNum - 1) * 10, 10);
  }

}
