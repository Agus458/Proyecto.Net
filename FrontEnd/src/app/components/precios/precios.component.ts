import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { PrecioDataType } from "src/app/models/PrecioDatatype";
import { PreciosService } from "src/app/services/precios/precios.service";

@Component({
  selector: 'app-precios',
  templateUrl: './precios.component.html',
  styleUrls: ['./precios.component.css']
})
export class PreciosComponent implements OnInit {

  precios: PrecioDataType[]
  page=1;
  size: number;
  productId:string;

  constructor(
    private PreciosService: PreciosService,
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    const routeParams = this.route.snapshot.paramMap;
    const IdFromRoute = routeParams.get('id');

    if (IdFromRoute) {
      this.productId = IdFromRoute;
      this.getPrecios(0, 10);
    }
  }
  
  getPrecios(skip: number, take: number) {
    this.PreciosService.getAll(skip, take, this.productId).subscribe(
      ok => {
        this.precios= ok.collection;
        this.size = ok.size;
      },
      error => {
        console.log(error);
      }
    );
  }

  onPageChange(pageNum: number): void {
    this.getPrecios((pageNum - 1) * 10, 10);
  }

  delete(id: string) {
    this.PreciosService.delete(id).subscribe(
      ok => {
        this.getPrecios(0, 10);
      }
    );
  }
  
}
