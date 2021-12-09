export class Time {
    hours: number;
    minutes: number;
    seconds: number;

    constructor(hours: number, minutes: number, seconds: number) {
        this.hours = hours;
        this.minutes = minutes;
        this.seconds = seconds;
    }
}

export enum RecurrencyType {
    UNIQUE,
    YEARLY,
    MONTHLY,
    WEEKLY
}

export class EventDataType {
    id: string;
    name: string;
    buildingId: string;
    startDate: Date;
    endDate: Date;
    startTime: any;
    endTime: any;
    monday: boolean;
    tuesday: boolean;
    wednesday: boolean;
    thursday: boolean;
    friday: boolean;
    saturday: boolean;
    sunday: boolean;
    recurrencyType: RecurrencyType;
}