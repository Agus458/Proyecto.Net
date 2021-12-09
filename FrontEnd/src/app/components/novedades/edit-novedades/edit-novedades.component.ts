import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NoveltiesService } from 'src/app/services/novelties.service';
import { ToastService } from 'src/app/services/toast/toast.service';
import { FormBuilder } from '@angular/forms';
import { Location } from '@angular/common';

@Component({
  selector: 'app-edit-novedades',
  templateUrl: './edit-novedades.component.html',
  styleUrls: ['./edit-novedades.component.css']
})
export class EditNovedadesComponent implements OnInit {

  NoveltyForm: FormGroup;
  buildingId: string;


  constructor(
    private NoveltiesService: NoveltiesService,
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

    this.NoveltyForm = this.FormBuilder.group({
      title: ["", [Validators.required]],
      content: ["", [Validators.required]],
      buildingId: [this.buildingId],
    });

    IdFromRoute = routeParams.get('noveltyId');

    if (IdFromRoute) {
      this.NoveltiesService.getById(IdFromRoute).subscribe(
        ok => {
          this.NoveltyForm.addControl("id", new FormControl('', [Validators.required]));
        }
      );
    }


  }

  submit() {
    console.log(this.NoveltyForm.value);

    if (this.NoveltyForm.contains("id")) {
      const id = this.NoveltyForm.controls.id.value;
      this.NoveltiesService.update(id, this.NoveltyForm.value).subscribe(
        ok => {
          this.toastService.show("Success", "Novedad actualizada");
          this.router.navigateByUrl("novedades");
        },
        err => this.toastService.show("Error", "Algo salio mal")
      );
    } else {
      this.NoveltiesService.create(this.NoveltyForm.value).subscribe(
        ok => {
          this.toastService.show("Success", "Novedad creada");
          this.router.navigateByUrl("novedades");
        },
        error => {
          console.log(error);

          this.toastService.show("Error", "Algo salio mal");
        }
      );
    }
  }


}
