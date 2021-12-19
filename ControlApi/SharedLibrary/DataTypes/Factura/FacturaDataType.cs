using SharedLibrary.DataTypes.Tenants;
using SharedLibrary.DataTypes.Pago;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Factura
{
    public record FacturaDataType
    {
        public Guid Id { get; init; }

        public DateTimeOffset CreatedDate { get; init; }

        public float Monto { get; init; }

        public string Descripcion { get; init; }

        public PagoDataType Pago { get; init; }
    }
}
