import { BuildingDataType } from "./BuildingDataType";
import { PersonDataType } from "./PersonDataType";

export class EntryDataType {
    id: string;

    person: PersonDataType;

    building: BuildingDataType;

    createdDate: Date;
}