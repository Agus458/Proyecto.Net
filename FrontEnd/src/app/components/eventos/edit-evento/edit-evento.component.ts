import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EventDataType, RecurrencyType, Time } from 'src/app/models/EventDataType';
import { EventosService } from 'src/app/services/eventos/eventos.service';

@Component({
  selector: 'app-edit-evento',
  templateUrl: './edit-evento.component.html',
  styleUrls: ['./edit-evento.component.css']
})
export class EditEventoComponent implements OnInit {

  roomId: string;

  event: EventDataType;

  eventForm: FormGroup;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private EventosService: EventosService,
    private FormBuilder: FormBuilder,
    public location: Location
  ) { }

  get recurrencyTypes() { return RecurrencyType };

  async ngOnInit() {
    const routeParams = this.route.snapshot.paramMap;
    let IdFromRoute = routeParams.get('id');

    if (IdFromRoute) {
      this.roomId = IdFromRoute;
    }

    this.eventForm = this.FormBuilder.group({
      roomId: [this.roomId, [Validators.required]],
      name: ["", [Validators.required]],
      startDate: ["", [Validators.required]],
      endDate: [null],
      startTime: ["", [Validators.required]],
      endTime: ["", [Validators.required]],
      monday: [false, [Validators.required]],
      tuesday: [false, [Validators.required]],
      wednesday: [false, [Validators.required]],
      thursday: [false, [Validators.required]],
      friday: [false, [Validators.required]],
      saturday: [false, [Validators.required]],
      sunday: [false, [Validators.required]],
      recurrencyType: ["", [Validators.required]],
    });

    IdFromRoute = routeParams.get('eventId');
    if (IdFromRoute) {
      try {
        this.event = await this.EventosService.getById(IdFromRoute, this.roomId).toPromise();

        this.event.startTime = this.getTime(this.event.startTime);
        this.event.endTime = this.getTime(this.event.endTime);

        // console.log(this.event);

        this.eventForm.patchValue(this.event);
      } catch (error) {
        console.log(error);
      }
    }
  }

  submit() {
    if (!this.event) {
      this.EventosService.create(this.eventForm.value).subscribe(
        ok => {
          console.log(ok);
          this.router.navigateByUrl("/eventos/salon/" + this.roomId);
        },
        error => {
          console.log(error);
        }
      );
    } else {
      this.EventosService.update(this.event.id, this.roomId, this.eventForm.value).subscribe(
        ok => {
          console.log(ok);
          this.router.navigateByUrl("/eventos/salon/" + this.roomId);
        },
        error => {
          console.log(error);
        }
      );
    }
  }

  getTime(time: any): string {
    return time.hours + ":" + time.minutes + ":" + time.seconds;
  }

}
