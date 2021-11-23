import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BuildingsService } from 'src/app/services/building/buildings.service';
import { ToastService } from 'src/app/services/toast/toast.service';

@Component({
  selector: 'app-edit-building',
  templateUrl: './edit-building.component.html',
  styleUrls: ['./edit-building.component.css']
})
export class EditBuildingComponent implements OnInit {

  buildingForm: FormGroup;
  latitude: number = -34.8833;
  longitude: number = -56.1667;

  constructor(
    private BuildingsService: BuildingsService,
    private FormBuilder: FormBuilder,
    public location: Location,
    private route: ActivatedRoute,
    private router: Router,
    private toastService: ToastService
  ) { }

  async ngOnInit() {
    this.buildingForm = this.FormBuilder.group({
      name: ["", [Validators.required]],
      latitude: ["", [Validators.required]],
      length: ["", [Validators.required]],
    });

    const routeParams = this.route.snapshot.paramMap;
    const IdFromRoute = routeParams.get('id');

    if (IdFromRoute) {
      this.BuildingsService.getById(IdFromRoute).subscribe(
        ok => {
          this.buildingForm.addControl("id", new FormControl('', [Validators.required]));
          this.latitude = ok.latitude;
          this.longitude = ok.longitude;
          this.buildingForm.patchValue(ok);
        }
      );
    }

    this.buildingForm.patchValue({ latitude: this.latitude, longitude: this.longitude });
  }

  submit() {
    console.log(this.buildingForm.value);
    
    if (this.buildingForm.contains("id")) {
      const id = this.buildingForm.controls.id.value;
      this.BuildingsService.update(id, this.buildingForm.value).subscribe(
        ok => {
          this.toastService.show("Success", "Edificio actualizado");
          this.router.navigateByUrl("/buildings");
        },
        err => this.toastService.show("Error", "Algo salio mal")
      );
    } else {
      this.BuildingsService.create(this.buildingForm.value).subscribe(
        ok => {
          this.toastService.show("Success", "Edificio creado");
          this.router.navigateByUrl("/buildings");
        },
        error => {
          console.log(error);
          
          this.toastService.show("Error", "Algo salio mal");
        }
      );
    }
  }

  onClickChange(event: any) {
    this.latitude = event.coords.lat;
    this.longitude = event.coords.lng;

    this.buildingForm.patchValue({ latitude: this.latitude, longitude: this.longitude });
  }
}
