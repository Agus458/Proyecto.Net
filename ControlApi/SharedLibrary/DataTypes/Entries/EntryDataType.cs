using SharedLibrary.DataTypes.Buildings;
using SharedLibrary.DataTypes.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Entries
{
    public record EntryDataType
    {
        public Guid Id { get; init; }

        public PersonDataType Person { get; init; }

        public BuildingDataType Building { get; init; }

        public DateTimeOffset CreatedDate { get; init; }
    }
}
