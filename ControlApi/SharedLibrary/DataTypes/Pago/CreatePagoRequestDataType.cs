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
        public float precio { set; get; }

        [Required]
        public DateTime fecha { set; get; }

        [Required]
        public bool Pagada { set; get; }

        [Required]
        public Guid FacturaId { get; init; }


    }
}
