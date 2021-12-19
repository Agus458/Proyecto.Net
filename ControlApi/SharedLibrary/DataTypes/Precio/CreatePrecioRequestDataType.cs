using SharedLibrary.DataTypes.Precio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Precio
{
    public record CreatePrecioRequestDataType
    {
        [Required]
        public float Amount { get; init; }

        [Required]
        public DateTime ValidDate { get; init; }

        [Required]
        public Guid ProductId { get; init; }
    }
       
}
