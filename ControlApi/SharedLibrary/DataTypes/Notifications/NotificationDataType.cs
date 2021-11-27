using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Notifications
{
    public record NotificationDataType
    {
        public Guid Id { get; init; }

        public string Message { get; init; }

        public bool Viewed { get; init; }
    }
}
