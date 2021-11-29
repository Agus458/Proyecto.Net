﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Entities
{
    public class Novelty : MustHaveBuildingEntity
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Image { get; set; }
    }
}
