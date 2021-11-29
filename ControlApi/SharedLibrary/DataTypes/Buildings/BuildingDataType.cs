using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Buildings
{
    public record BuildingDataType
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public float Latitude { get; init; }

        public float Longitude { get; init; }
    }
}
