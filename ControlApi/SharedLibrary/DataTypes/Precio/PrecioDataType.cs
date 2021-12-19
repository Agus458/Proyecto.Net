using SharedLibrary.DataTypes.Products;
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

        public float Amount { get; init; }

        public DateTime ValidDate { get; init; }
    }
}
