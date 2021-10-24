using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Precio
{
    public record PrecioDatatype
    {
        [MaxLength(20)]
        float monto;

        DateTime fecha;
    }
}
