using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Factura
{
    public record UpdateFacturaRequestDataType
    {
        public DateTime fecha { get; init; }
        public float monto { get; init; }           
    }
}
