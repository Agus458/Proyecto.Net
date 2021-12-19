using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Entities
{
    public class Factura : MustHaveTenantEntity
    {
        public float Monto { get; set; }

        public string Descripcion { get; set; }

        public Pago Pago { get; set; }
    }
}
