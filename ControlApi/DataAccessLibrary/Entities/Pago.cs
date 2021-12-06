using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Entities
{
    public class Pago : BaseEntity
    {
    public DateTime Date { get; set; }

        public Guid FacturaId { get; set; }

        public virtual Factura Factura { get; set; }
        public float Monto { get; set; }
    }
}
