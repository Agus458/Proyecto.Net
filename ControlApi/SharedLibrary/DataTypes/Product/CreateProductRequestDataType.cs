using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Product
{
    public record CreateProductRequestDataType
    {
        [Required]
        [MaxLength(12)]
        public string Nombre { get; init; }

       
        
        


    }
}
