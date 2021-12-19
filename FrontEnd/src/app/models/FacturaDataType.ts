import { PagoDataType } from "./PagoDataType";

export class FacturaDataType {
    id: string;

    createdDate: Date;

    monto: number;

    pago: PagoDataType;

    descripcion: string;
}