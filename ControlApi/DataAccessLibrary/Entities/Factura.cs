using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Entities
{
     public class Factura : BaseEntity
    {
        public DateTime fecha { get; set; }


        public float Monto { get; set; }
    }
}
