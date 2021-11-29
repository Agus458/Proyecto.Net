import { Component, OnInit } from '@angular/core';
import { WebcamImage } from 'ngx-webcam';
import { PersonasService } from 'src/app/services/personas/personas.service';
import { ToastService } from 'src/app/services/toast/toast.service';

@Component({
  selector: 'app-ingresar',
  templateUrl: './ingresar.component.html',
  styleUrls: ['./ingresar.component.css']
})
export class IngresarComponent implements OnInit {

  constructor(
    private PersonasService: PersonasService,
    private toastService: ToastService
  ) { }

  ngOnInit(): void {
  }

  async handleImage(webcamImage: WebcamImage) {
    var image = await this.dataUrlToFile(webcamImage.imageAsDataUrl, "imagen")
    this.PersonasService.identify(image).subscribe(
      ok => {
        this.toastService.show("Success", "Bienvenido " + ok.name + " " + ok.lastName);
      },
      error => {
        console.log(error);

        this.toastService.show("Error", "Algo salio mal");
      }
    );
  }

  async dataUrlToFile(dataUrl: string, fileName: string): Promise<File> {
    const res: Response = await fetch(dataUrl);
    const blob: Blob = await res.blob();
    return new File([blob], fileName, { type: 'image/jpg' });
  }
}
