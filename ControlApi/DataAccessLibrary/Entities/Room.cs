﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Entities
{
    public class Room : MustHaveTenantEntity
    {
        public Guid BuildingId { get; set; }
        public virtual Building Building { get; set; }
    }
}
