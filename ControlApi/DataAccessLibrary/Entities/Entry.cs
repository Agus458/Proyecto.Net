using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Entities
{
    public class Entry : MustHaveBuildingEntity
    {
        public Guid PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}
