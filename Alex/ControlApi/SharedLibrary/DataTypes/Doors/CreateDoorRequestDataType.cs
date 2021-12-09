using SharedLibrary.DataTypes.Buildings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Doors
{
    public record CreateDoorRequestDataType
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; init; }

        [Required]
        public Guid BuildingId { get; init; }
    }
}
