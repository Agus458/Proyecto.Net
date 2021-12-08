using DataAccessLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Events
{
    public record EventDataType
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public DateTime StartDate { get; init; }

        public DateTime EndDate { get; init; }

        public TimeSpan StartTime { get; init; }

        public TimeSpan EndTime { get; init; }

        public bool Monday { get; init; }

        public bool Tuesday { get; init; }

        public bool Wednesday { get; init; }

        public bool Thursday { get; init; }

        public bool Friday { get; init; }

        public bool Saturday { get; init; }

        public bool Sunday { get; init; }

        public RecurrencyType RecurrencyType { get; init; }
    }
}
