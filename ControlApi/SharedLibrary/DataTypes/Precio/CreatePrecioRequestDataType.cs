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

    public float Monto { get; init; }
        
    [Required]
       public DateTime fecha { get; init; }
    }
    

}
