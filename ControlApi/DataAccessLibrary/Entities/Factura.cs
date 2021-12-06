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
        public DateTime fecha { get; set; }

        public float Monto { get; set; }

        public Pago PagoFactura { get; init; }
    }

}
