using SharedLibrary.DataTypes.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Products
{
    public record CreateProductsRequestDataType
    {
    [Required]
    public string name { get; init; }

   
    }
}
