import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { CalendarEvent } from 'angular-calendar';
import moment from 'moment';
import RRule from 'rrule';
import { EventDataType, RecurrencyType } from 'src/app/models/EventDataType';
import { CalendarUtils } from 'src/app/utils/calendar.util';

@Component({
  selector: 'app-eventos-institucion',
  templateUrl: './eventos-institucion.component.html',
  styleUrls: ['./eventos-institucion.component.css']
})
export class EventosInstitucionComponent implements OnInit, OnChanges {

  @Input() events: EventDataType[];

  calendarEvents: CalendarEvent[] = [];

  constructor() { }

  ngOnChanges(changes: SimpleChanges): void {
    this.calendarEvents = [];

    if (this.events) {
      this.events.forEach(event => {
        if (event.recurrencyType == RecurrencyType.UNIQUE) {
          this.calendarEvents.push({
            title: event.name + " - De: " + this.getTime(event.startTime) + " A: " + this.getTime(event.endTime) + " Salon: " + event.room.name + " Edificio: " + event.room.building.name,
            start: moment(event.startDate).toDate(),
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
            this.calendarEvents.push({
              title: event.name + " - De: " + this.getTime(event.startTime) + " A: " + this.getTime(event.endTime) + " Salon: " + event.room.name + " Edificio: " + event.room.building.name,
              start: date,
              id: event.id
            });
          })
        }
      });
    }
  }

  ngOnInit(): void {

  }

  getTime(time: any): string {
    return time.hours + ":" + time.minutes + ":" + time.seconds;
  }
}
