import { Component, OnInit } from '@angular/core';
import { FacturaDataType } from 'src/app/models/FacturaDataType';
import { FacturasService } from 'src/app/services/facturas/facturas.service';

@Component({
  selector: 'app-facturas',
  templateUrl: './facturas.component.html',
  styleUrls: ['./facturas.component.css']
})
export class FacturasComponent implements OnInit {

  facturas: FacturaDataType[];
  page = 1;
  size: number;

  constructor(
    private FacturasService: FacturasService
  ) { }

  ngOnInit(): void {
    this.getFacturas(0, 10);
  }

  getFacturas(skip: number, take: number) {
    this.FacturasService.getAll(skip, take).subscribe(
      ok => {
        this.facturas = ok.collection;
        this.size = ok.size;
      },
      error => {
        console.log(error);
      }
    );
  }

  onPageChange(pageNum: number): void {
    this.getFacturas((pageNum - 1) * 10, 10);
  }

}
