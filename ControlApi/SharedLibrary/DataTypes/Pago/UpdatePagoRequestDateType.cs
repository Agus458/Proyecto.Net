using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Pago
{
    public record UpdatePagoRequestDateType
    {
        public float Monto { set; get; }
    }
}
