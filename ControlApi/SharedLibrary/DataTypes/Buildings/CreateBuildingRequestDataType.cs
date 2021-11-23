using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Buildings
{
    public record CreateBuildingRequestDataType
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; init; }

        [Required]
        public float Latitude { get; init; }

        [Required]
        public float Longitude { get; init; }
    }
}
