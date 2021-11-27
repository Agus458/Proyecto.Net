using SharedLibrary.DataTypes.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Precio
{
    public record PrecioDataType
    {
        public Guid Id { get; init; }
        public float precio { set; get; }

        public DateTime Fecha{ set; get; }

    }
}
