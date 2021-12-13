import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SalonDataType } from 'src/app/models/SalonDataType';
import { SalonesService } from 'src/app/services/salones/salones.service';

@Component({
  selector: 'app-edit-salon',
  templateUrl: './edit-salon.component.html',
  styleUrls: ['./edit-salon.component.css']
})
export class EditSalonComponent implements OnInit {

  buildingId: string;

  salon: SalonDataType;

  salonForm: FormGroup;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private SalonesService: SalonesService,
    private FormBuilder: FormBuilder,
    public location: Location
  ) { }

  async ngOnInit() {
    const routeParams = this.route.snapshot.paramMap;
    let IdFromRoute = routeParams.get('id');

    if (IdFromRoute) {
      this.buildingId = IdFromRoute;
    }

    this.salonForm = this.FormBuilder.group({
      buildingId: [this.buildingId, [Validators.required]],
      name: ["", [Validators.required]],
    });

    IdFromRoute = routeParams.get('salonId');
    if (IdFromRoute) {
      try {
        this.salon = await this.SalonesService.getById(IdFromRoute).toPromise();
        this.salonForm.patchValue(this.salon);
      } catch (error) {
        console.log(error);
      }
    }
  }

  submit() {
    if (!this.salon) {
      this.SalonesService.create(this.salonForm.value).subscribe(
        ok => {
          this.router.navigateByUrl("/salones/edificio/" + this.buildingId);
        },
        error => {
          console.log(error);
        }
      );
    } else {
      this.SalonesService.update(this.salon.id, this.salonForm.value).subscribe(
        ok => {
          this.router.navigateByUrl("/salones/edificio/" + this.buildingId);
        },
        error => {
          console.log(error);
        }
      );
    }
  }

}
