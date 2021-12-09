import { PagoDataType } from "./PagoDataType";

export class FacturaDataType {
    id:string;
    
    fecha:Date;
    
    monto:number;

    pago:PagoDataType;
    
}