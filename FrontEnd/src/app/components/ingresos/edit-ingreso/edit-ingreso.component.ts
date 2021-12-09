import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { PersonDataType } from 'src/app/models/PersonDataType';
import { IngresosService } from 'src/app/services/ingresos/ingresos.service';
import { PersonasService } from 'src/app/services/personas/personas.service';

@Component({
  selector: 'app-edit-ingreso',
  templateUrl: './edit-ingreso.component.html',
  styleUrls: ['./edit-ingreso.component.css']
})
export class EditIngresoComponent implements OnInit {

  ingresoForm: FormGroup;
  page = 1;
  size: number;
  personas: PersonDataType[];
  selectedPersona: PersonDataType | undefined;

  constructor(
    private router: Router,
    private IngresosService: IngresosService,
    private FormBuilder: FormBuilder,
    public location: Location,
    private PersonasService: PersonasService
  ) { }

  ngOnInit() {
    this.ingresoForm = this.FormBuilder.group({
      personId: ["", [Validators.required]]
    });

    this.getPersons(0, 10);
  }

  submit() {
    this.IngresosService.create(this.ingresoForm.value).subscribe(
      ok => {
        console.log(ok);
        this.router.navigateByUrl("/ingresos")
      },
      error => {
        console.log(error);
      }
    );
  }

  getPersons(skip: number, take: number) {
    this.PersonasService.getAll(skip, take).subscribe(
      ok => {
        this.personas = ok.collection;
        this.size = ok.size;
      },
      error => {
        console.log(error);
      }
    );
  }

  onPageChange(pageNum: number): void {
    this.getPersons((pageNum - 1) * 10, 10);
  }

  select(persona: PersonDataType) {
    this.selectedPersona = persona;
    this.ingresoForm.get("personId")?.setValue(persona.id);
  }

  clear() {
    this.selectedPersona = undefined;
    this.ingresoForm.get("personId")?.setValue("");
  }

}
