using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Entities
{
     public class Evento: BaseEntity
    {
        public bool recurrente { get; set; }

        public DateTime fecha { get; set; }
    }
      
}
