import RRule, { ByWeekday, Frequency } from "rrule";
import { EventDataType, RecurrencyType } from "../models/EventDataType";

export class CalendarUtils {

    public static ToFreq(recurrencyType: RecurrencyType): Frequency | undefined {
        switch (recurrencyType) {
            case RecurrencyType.YEARLY:
                return Frequency.YEARLY;

            case RecurrencyType.MONTHLY:
                return Frequency.MONTHLY;

            case RecurrencyType.WEEKLY:
                return Frequency.WEEKLY;

            default:
                return Frequency.DAILY;
        }
    }

    public static GetDays(event: EventDataType): ByWeekday[] | undefined {
        const days: ByWeekday[] = [];

        if (event.monday) days.push(RRule.MO);
        if (event.tuesday) days.push(RRule.TU);
        if (event.wednesday) days.push(RRule.WE);
        if (event.thursday) days.push(RRule.TH);
        if (event.friday) days.push(RRule.FR);
        if (event.saturday) days.push(RRule.SA);
        if (event.sunday) days.push(RRule.SU);

        if(days.length == 0) return undefined;

        return days;
    }

}