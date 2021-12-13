using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Events
{
    public record UpdateEventRequestDataType
    {
        public string Name { get; init; }

        public string StartDate { get; init; }

        public string EndDate { get; init; }

        public string StartTime { get; init; }

        public string EndTime { get; init; }

        public bool Monday { get; init; }

        public bool Tuesday { get; init; }

        public bool Wednesday { get; init; }

        public bool Thursday { get; init; }

        public bool Friday { get; init; }

        public bool Saturday { get; init; }

        public bool Sunday { get; init; }

        public int RecurrencyType { get; init; }
    }
}
