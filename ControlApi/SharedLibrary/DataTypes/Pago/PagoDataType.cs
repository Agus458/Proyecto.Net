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
       
        public DateTime fecha { set; get; }
        
        public float monto { set; get; }

    }
}
