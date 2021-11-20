using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Doors
{
    public record UpdateDoorRequestDataType
    {
        [MaxLength(200)]
        public string Name { get; init; }

        public Guid? BuildingId { get; init; }
    }
}
