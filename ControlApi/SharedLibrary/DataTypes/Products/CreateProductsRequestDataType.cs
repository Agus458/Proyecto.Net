﻿using SharedLibrary.DataTypes.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Products
{
    public record CreateProductsRequestDataType
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; init; }

        [Required]
        public int CantBuildings { get; init; }

        [Required]
        public int CantRooms { get; init; }
    }
}
