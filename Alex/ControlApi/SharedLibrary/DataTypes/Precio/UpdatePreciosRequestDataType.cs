using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Precio
{
    public record UpdatePreciosRequestDataType
    {
        public float Precio { get; init; }
        public DateTime fecha { get; init; }
    }
}
