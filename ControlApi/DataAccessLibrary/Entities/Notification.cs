﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Entities
{
    public class Notification : BaseEntity
    {
        public string Message { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
