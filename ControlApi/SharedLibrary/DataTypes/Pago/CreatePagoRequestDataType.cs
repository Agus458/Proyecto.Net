using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Pago
{
    public record CreatePagoRequestDataType
    {
        [Required]
        public float Monto { get; init; }

        [Required]
        public Guid FacturaId { get; init; }
    }
}
