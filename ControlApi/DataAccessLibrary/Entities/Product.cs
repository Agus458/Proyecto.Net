using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Entities
{
    public class Product : BaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; }

        public List<Precio> Precios { get; set; }

        public int CantBuildings { get; set; }
    }
    
}
