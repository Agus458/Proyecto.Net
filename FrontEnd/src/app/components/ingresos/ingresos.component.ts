import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EntryDataType } from 'src/app/models/EntryDataType';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';
import { IngresosService } from 'src/app/services/ingresos/ingresos.service';

@Component({
  selector: 'app-ingresos',
  templateUrl: './ingresos.component.html',
  styleUrls: ['./ingresos.component.css']
})
export class IngresosComponent implements OnInit {

  buildingId: string;
  page = 1;
  size: number;
  entries: EntryDataType[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private IngresosService: IngresosService,
    public AuthenticationService: AuthenticationService
  ) { }

  ngOnInit(): void {
    const routeParams = this.route.snapshot.paramMap;
    const IdFromRoute = routeParams.get('id');

    if (IdFromRoute) {
      this.buildingId = IdFromRoute;
    }

    this.getEntries(0, 10);
  }

  getEntries(skip: number, take: number) {
    if (this.AuthenticationService.hasRole("Admin")) {
      this.IngresosService.getAll(skip, take, this.buildingId).subscribe(
        ok => {
          this.entries = ok.collection;
          this.size = ok.size;
        }
      );
    } else {
      this.IngresosService.get(skip, take).subscribe(
        ok => {
          this.entries = ok.collection;
          this.size = ok.size;
        }
      );
    }
  }

  onPageChange(pageNum: number): void {
    this.getEntries((pageNum - 1) * 10, 10);
  }

  delete(id: string){
    this.IngresosService.delete(id).subscribe();
    window.location.reload();
  }

}
