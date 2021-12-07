using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Events
{
    public record CreateEventRequestDataType
    {
        [Required]
        public string Name { get; init; }

        [Required]
        public Guid BuildingId { get; init; }

        [Required]
        public string StartDate { get; init; }

        public string EndDate { get; init; }

        [Required]
        public string StartTime { get; init; }

        [Required]
        public string EndTime { get; init; }

        public bool Monday { get; init; }

        public bool Tuesday { get; init; }

        public bool Wednesday { get; init; }

        public bool Thursday { get; init; }

        public bool Friday { get; init; }

        public bool Saturday { get; init; }

        public bool Sunday { get; init; }

        [Required]
        public int RecurrencyType { get; init; }
    }
}
