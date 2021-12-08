import {
  Component,
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Input,
  Output,
  EventEmitter
} from '@angular/core';
import moment from 'moment-timezone';
import {
  CalendarEvent,
  CalendarView,
} from 'angular-calendar';
import { ViewPeriod } from 'calendar-utils';

export interface RecurringEvent {
  title: string;
  rrule?: {
    freq: any;
    bymonth?: number;
    bymonthday?: number;
    byweekday?: any;
  };
  dtstart: Date;
  until: Date;
}

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent {

  @Output() eventEvent = new EventEmitter<CalendarEvent>();

  @Input() addRoute: string;

  view: CalendarView = CalendarView.Month;

  viewDate = moment().toDate();

  @Input() calendarEvents: CalendarEvent[] = [];

  viewPeriod: ViewPeriod;

  constructor(private cdr: ChangeDetectorRef) { }

  activeDayIsOpen: boolean = false;

  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    if (moment(date).month == moment(this.viewDate).month) {
      if (
        ((moment(this.viewDate).day == moment(date).day) && this.activeDayIsOpen === true) ||
        events.length === 0
      ) {
        this.activeDayIsOpen = false;
      } else {
        this.activeDayIsOpen = true;
      }
      this.viewDate = date;
    }
  }

  onClick(event: CalendarEvent): void {
    this.eventEvent.emit(event);
  }

  closeOpenMonthViewDay() {
    this.activeDayIsOpen = false;
  }
}
