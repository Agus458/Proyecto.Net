using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Factura
{
    public record FacturaDataType
    {
        public DateTime fecha { set; get; }
        public float monto { set; get; }
    }
}
