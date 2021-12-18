import { ProductosDataType } from "./ProductosDataType";

export interface TenantDataType {
    id: string
    rut: string
    socialReason: string
    productId: string
    product: ProductosDataType
}