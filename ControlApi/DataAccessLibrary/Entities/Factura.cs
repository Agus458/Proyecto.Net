﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataAccessLibrary.Entities
{

    public class Factura : MustHaveTenantEntity
    {
        public DateTime fecha { get; set; }

        public float Monto { get; set; }
        public Guid BuildingId { get; set; }

        public virtual Building Building{ get; set;}
    }

}
