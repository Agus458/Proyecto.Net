using SharedLibrary.DataTypes.Factura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Pago
{
    public record PagoDataType
    {
   
        public Guid Id { get; init; }
       
        public DateTimeOffset CreatedDate { set; get; }
        
        public float Monto { set; get; }

        public Guid FacturaId { get; init; }
    }
}
