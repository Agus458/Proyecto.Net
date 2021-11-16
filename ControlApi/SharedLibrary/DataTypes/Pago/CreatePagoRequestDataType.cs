using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Pago
{
    public record CreatePagoRequestDataType
    {
        public float precio { set; get; }

        public DateTime fecha { set; get; }
    }
}
