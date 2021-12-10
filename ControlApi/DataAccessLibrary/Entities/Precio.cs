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
        [MaxLength(200)]
        public float precio { get; set; }

        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }

    }
   
     
}
