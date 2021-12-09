import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CalendarEvent } from 'calendar-utils';
import moment from 'moment-timezone';
import RRule from 'rrule';
import { EventDataType } from 'src/app/models/EventDataType';
import { EventosService } from 'src/app/services/eventos/eventos.service';
import { CalendarUtils } from 'src/app/utils/calendar.util';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  buildingId: string;
  page = 1;
  size: number;
  events: CalendarEvent[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private EventosService: EventosService
  ) { }

  ngOnInit(): void {
    const routeParams = this.route.snapshot.paramMap;
    const IdFromRoute = routeParams.get('id');

    if (IdFromRoute) {
      this.buildingId = IdFromRoute;

      this.getEvents();
    }
  }

  getEvents() {
    this.EventosService.getAll(this.buildingId).subscribe(
      ok => {
        console.log(ok);

        const events: CalendarEvent[] = [];

        ok.forEach(event => {
          const rule = new RRule({
            freq: CalendarUtils.ToFreq(event.recurrencyType),
            dtstart: moment(event.startDate).toDate(),
            until: moment(event.endDate).toDate(),
            byweekday: CalendarUtils.GetDays(event)
          });

          rule.all().forEach(date => {
            events.push({
              title: event.name,
              start: date,
              id: event.id
            });
          })
        });

        this.events = events;
      }
    );
  }

  openEvent(event: CalendarEvent) {
    this.router.navigateByUrl("/eventos/edificio/" + this.buildingId + "/editar/" + event.id);
  }
}
