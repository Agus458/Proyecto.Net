using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Entities
{
   public  class Portero : MustHaveTenantEntity
    {
        [MaxLength(200)]
        public string Name { get; set; }


        [MaxLength(200)]
        public string Apellido{ get; set; }

        public Guid PersonsId { get; set; }

        public virtual Person Person { get; set; }
    }
}
