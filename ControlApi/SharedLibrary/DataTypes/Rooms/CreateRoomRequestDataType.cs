﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Rooms
{
    public record CreateRoomRequestDataType
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public Guid BuildingId { get; init; }
    }
}
