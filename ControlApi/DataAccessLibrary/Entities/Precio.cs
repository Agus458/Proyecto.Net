using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Entities
{
    public class Precio :BaseEntity
    {
        //[MaxLength(200)]
        public DateTime Fecha { get; set; }

        public float Monto { get; set; }

    }
      
}
