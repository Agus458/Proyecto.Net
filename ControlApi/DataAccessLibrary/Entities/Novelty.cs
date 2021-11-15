using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Entities
{
    public class Novelty : MustHaveTenantEntity
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Image { get; set; }

        public Guid BuildingId { get; set; }

        public virtual Building Building { get; set; }
    }
}
