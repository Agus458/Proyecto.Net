using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Entities
{
    public class Product : BaseEntity
    {


    [MaxLength(200)]
    public string Nombre { get; set; }




    }
}
