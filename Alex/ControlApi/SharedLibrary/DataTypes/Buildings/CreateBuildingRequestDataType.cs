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
        [MaxLength(200)]
        public string Latitude { get; init; }

        [Required]
        [MaxLength(200)]
        public string Length { get; init; }
    }
}
