import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CalendarEventAction } from 'angular-calendar';
import { CalendarEvent } from 'calendar-utils';
import moment from 'moment-timezone';
import RRule from 'rrule';
import { EventDataType, RecurrencyType } from 'src/app/models/EventDataType';
import { EventosService } from 'src/app/services/eventos/eventos.service';
import { CalendarUtils } from 'src/app/utils/calendar.util';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  salonId: string;
  page = 1;
  size: number;
  events: CalendarEvent[] = [];

  actions: CalendarEventAction[] = [
    {
      label: '<i class="fas fa-fw fa-trash-alt"></i>',
      a11yLabel: 'Delete',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.delete(event);
      },
    },
  ];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private EventosService: EventosService
  ) { }

  ngOnInit(): void {
    const routeParams = this.route.snapshot.paramMap;
    const IdFromRoute = routeParams.get('id');

    if (IdFromRoute) {
      this.salonId = IdFromRoute;

      this.getEvents();
    }
  }

  getEvents() {
    this.EventosService.getAll(this.salonId).subscribe(
      ok => {
        console.log(ok);

        const events: CalendarEvent[] = [];

        ok.forEach(event => {
          if (event.recurrencyType == RecurrencyType.UNIQUE) {
            events.push({
              title: event.name + " - De: " + this.getTime(event.startTime) + " A: " + this.getTime(event.endTime),
              start: moment(event.startDate).toDate(),
              actions: this.actions,
              id: event.id
            });
          } else {
            const rule = new RRule({
              freq: CalendarUtils.ToFreq(event.recurrencyType),
              dtstart: moment(event.startDate).toDate(),
              until: moment(event.endDate).toDate(),
              byweekday: CalendarUtils.GetDays(event),
              interval: 1
            });

            rule.all().forEach(date => {
              events.push({
                title: event.name + " - De: " + this.getTime(event.startTime) + " A: " + this.getTime(event.endTime),
                start: date,
                actions: this.actions,
                id: event.id
              });
            })
          }
        });

        this.events = events;
      }
    );
  }

  openEvent(event: CalendarEvent) {
    this.router.navigateByUrl("/eventos/salon/" + this.salonId + "/editar/" + event.id);
  }

  delete(event: CalendarEvent) {
    if (typeof event.id == "string") {
      this.EventosService.delete(event.id, this.salonId).subscribe(
        ok => this.getEvents()
      );
    }
  }

  getTime(time: any): string {
    return time.hours + ":" + time.minutes + ":" + time.seconds;
  }
}
