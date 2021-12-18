using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Products
{
    public record UpdateProductsRequestDataType
    {
        [MaxLength(200)]
        public string Name { get; init; }

        public int CantBuildings { get; init; }

        public int CantRooms { get; init; }
    }
}
