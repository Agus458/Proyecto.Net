﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Novelties
{
    public record UpdateNoveltyRequestDataType
    {
        [MaxLength(200)]
        public string Title { get; init; }

        public string Content { get; init; }

        public Guid? BuildingId { get; init; }

        public IFormFile FileImage { get; init; }
    }
}
