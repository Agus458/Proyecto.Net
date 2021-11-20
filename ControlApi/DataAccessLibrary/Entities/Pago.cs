using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Entities
{
    public class Pago : MustHaveTenantEntity
    {
    public DateTime Date { get; set; }

        public Guid BuildingId { get; set; }

        public virtual Building Building { get; set; }
    }
}
