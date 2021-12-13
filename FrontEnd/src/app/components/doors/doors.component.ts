import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DoorDataType } from 'src/app/models/DoorDataType';
import { DoorsService } from 'src/app/services/doors/doors.service';
import { ToastService } from 'src/app/services/toast/toast.service';

@Component({
  selector: 'app-doors',
  templateUrl: './doors.component.html',
  styleUrls: ['./doors.component.css']
})
export class DoorsComponent implements OnInit {

  doors: DoorDataType[];
  selectedDoor: DoorDataType;
  page = 1;
  size: number;
  buildingId: string;

  constructor(
    private DoorsService: DoorsService,
    private modalService: NgbModal,
    private router: Router,
    private toastService: ToastService,
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    const routeParams = this.route.snapshot.paramMap;
    const IdFromRoute = routeParams.get('id');

    if (IdFromRoute) {
      this.buildingId = IdFromRoute;
      this.getDoors(0, 10);
    }
  }

  getDoors(skip: number, take: number) {
    this.DoorsService.getAll(skip, take, this.buildingId).subscribe(
      ok => {
        this.doors = ok.collection.sort((a, b)=>{
            if (a.name > b.name) {
              return 1;
            }
            if (a.name < b.name) {
              return -1;
            }
            // a must be equal to b
            return 0;
        });
        this.size = ok.size;
      },
      error => {
        console.log(error);
      }
    );
  }


  delete(id: string) {
    this.DoorsService.delete(id).subscribe(
      ok => {
        this.toastService.show("Success", "Puerta eliminada");
        this.modalService.dismissAll();
        this.getDoors(0, 10);
      },
      error => {
        console.log(error);

        this.toastService.show("Error", "Algo salio mal");
      }
    );
  }

  onPageChange(pageNum: number): void {
    this.getDoors((pageNum - 1) * 10, 10);
  }

}
