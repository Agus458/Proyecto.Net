using SharedLibrary.DataTypes.Precio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Products
{
    public record ProductsDataType
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public int CantBuildings { get; init; }

        public int CantRooms { get; init; }

        public List<PrecioDataType> Precios { get; init; }
    }
}
