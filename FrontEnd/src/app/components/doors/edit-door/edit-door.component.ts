import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from 'src/app/services/toast/toast.service';
import { DoorsService } from 'src/app/services/doors/doors.service';


@Component({
  selector: 'app-edit-door',
  templateUrl: './edit-door.component.html',
  styleUrls: ['./edit-door.component.css']
})
export class EditDoorComponent implements OnInit {

  DoorForm: FormGroup;
  buildingId: string;


  constructor(
    private DoorsService: DoorsService,
    private FormBuilder: FormBuilder,
    public location: Location,
    private route: ActivatedRoute,
    private router: Router,
    private toastService: ToastService
  ) { }

  async ngOnInit() {
    
    const routeParams = this.route.snapshot.paramMap;
    let IdFromRoute = routeParams.get('id');

    if (IdFromRoute) {
      this.buildingId = IdFromRoute;
    }

    this.DoorForm = this.FormBuilder.group({
      name: ["", [Validators.required]],
      buildingId: [this.buildingId],
    });

    IdFromRoute = routeParams.get('doorId');

    if (IdFromRoute) {
      this.DoorsService.getById(IdFromRoute).subscribe(
        ok => {
          this.DoorForm.addControl("id", new FormControl('', [Validators.required]));
        }
      );
    }


  }

  submit() {
    console.log(this.DoorForm.value);

    if (this.DoorForm.contains("id")) {
      const id = this.DoorForm.controls.id.value;
      this.DoorsService.update(id, this.DoorForm.value).subscribe(
        ok => {
          this.toastService.show("Success", "Puerta actualizada");
          this.router.navigateByUrl("/puertas/edificio/" + this.buildingId);
        },
        err => this.toastService.show("Error", "Algo salio mal")
      );
    } else {
      this.DoorsService.create(this.DoorForm.value).subscribe(
        ok => {
          this.toastService.show("Success", "Puerta creada");
          this.router.navigateByUrl("/puertas/edificio/" + this.buildingId);
        },
        error => {
          console.log(error);

          this.toastService.show("Error", "Algo salio mal");
        }
      );
    }
  }


}

