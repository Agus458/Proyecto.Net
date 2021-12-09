using DataAccessLibrary.Entities;
using SharedLibrary.DataTypes.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Rooms
{
    public record RoomDataType
    {

        public Guid Id { get; init; }

        public string Name { get; set; }

        public Guid BuildingId { get; init; }

        public BuildingDataType Building { get; init; }
    }
}
