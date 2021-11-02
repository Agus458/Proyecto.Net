﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Entities
{
    public class Door : BaseEntity
    {
        public string Name { get; set; }

        public Guid BuildingId { get; set; }

        public Building Building { get; set; }
    }
}
