import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { WebcamImage } from 'ngx-webcam';
import { PersonasService } from 'src/app/services/personas/personas.service';
import { ToastService } from 'src/app/services/toast/toast.service';

@Component({
  selector: 'app-nuevapersona',
  templateUrl: './nuevapersona.component.html',
  styleUrls: ['./nuevapersona.component.css']
})
export class NuevapersonaComponent implements OnInit {

  personForm: FormGroup;

  constructor(
    private PersonService: PersonasService,
    private FormBuilder: FormBuilder,
    public location: Location,
    private route: ActivatedRoute,
    private router: Router,
    private toastService: ToastService
  ) { }

  async ngOnInit() {
    this.personForm = this.FormBuilder.group({
      name: ["", [Validators.required]],
      document: ["", [Validators.required]],
      documentType: ["", [Validators.required]],
      lastName: ["", [Validators.required]],
      phone: ["", [Validators.required]],
      email: ["", [Validators.required, Validators.email]],
      fileImage: []
    });

    const routeParams = this.route.snapshot.paramMap;
    const IdFromRoute = routeParams.get('id');

    if (IdFromRoute) {
      this.PersonService.getById(IdFromRoute).subscribe(
        ok => {
          this.personForm.addControl("id", new FormControl('', [Validators.required]));
          this.personForm.patchValue(ok);
        }
      );
    }
  }

  submit() {
    console.log(this.personForm.value);
    

    if (this.personForm.contains("id")) {
      const id = this.personForm.controls.id.value;
      this.PersonService.update(id, this.personForm.value).subscribe(
        ok => {
          this.toastService.show("Success", "Persona actualizada");
          this.router.navigateByUrl("/personas");
        },
        err => this.toastService.show("Error", "Algo salio mal")
      );
    } else {
      this.PersonService.create(this.personForm.value).subscribe(
        ok => {
          this.toastService.show("Success", "Persona creada");
          this.router.navigateByUrl("/personas");
        },
        error => {
          console.log(error);

          this.toastService.show("Error", "Algo salio mal");
        }
      );
    }
  }

  public webcamImage: WebcamImage;
  async handleImage(webcamImage: WebcamImage) {
    var image = await this.dataUrlToFile(webcamImage.imageAsDataUrl, "imagen.jpg")
    
    this.personForm.get('fileImage')?.setValue(image);
    this.webcamImage = webcamImage;
  }

  async dataUrlToFile(dataUrl: string, fileName: string): Promise<File> {
    const res: Response = await fetch(dataUrl);
    const blob: Blob = await res.blob();
    return new File([blob], fileName, { type: 'image/jpg' });
  }

}
