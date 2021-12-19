using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Entities
{
    public class Precio : BaseEntity
    {
        public float Amount { get; set; }

        public DateTime ValidDate { get; set; }

        public Guid ProductId { get; set; }

        public Product Product { get; set; }

    }
   
     
}
