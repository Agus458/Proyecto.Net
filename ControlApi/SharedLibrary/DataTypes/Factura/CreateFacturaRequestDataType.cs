using SharedLibrary.DataTypes.Factura;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Factura
{
    public record CreateFacturaRequestDataType
    {
        [Required]
        public DateTime fecha { get; init; }
        [Required]
        public float monto { get; init; }

        
    }
}
