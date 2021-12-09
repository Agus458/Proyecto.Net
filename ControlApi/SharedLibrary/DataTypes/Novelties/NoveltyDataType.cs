    using DataAccessLibrary.Entities;
using SharedLibrary.DataTypes.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Novelties
{
    public record NoveltyDataType
    {

        public Guid Id { get; init; }
        public string Title { get; init; }

        public string Content { get; init; }

        public string Image { get; init; }

        public Guid BuildingId { get; init; }

        public BuildingDataType Building { get; init; }
    }
}
