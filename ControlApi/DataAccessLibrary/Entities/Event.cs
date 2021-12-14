using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Entities
{
    public class Event : BaseEntity
    {
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public bool Monday { get; set; }

        public bool Tuesday { get; set; }

        public bool Wednesday { get; set; }

        public bool Thursday { get; set; }

        public bool Friday { get; set; }

        public bool Saturday { get; set; }

        public bool Sunday { get; set; }

        public RecurrencyType RecurrencyType { get; set; }

        public Guid RoomId { get; set; }

        public Room Room { get; set; }
    }

    public enum RecurrencyType
    {
        UNIQUE,
        YEARLY,
        MONTHLY,
        WEEKLY
    }
}
