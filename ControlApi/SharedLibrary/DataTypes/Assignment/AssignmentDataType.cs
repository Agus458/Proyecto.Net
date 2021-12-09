using SharedLibrary.DataTypes.Doors;
using SharedLibrary.DataTypes.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Assignment
{
    public record AssignmentDataType
    {
        public Guid Id { get; init; }

        public Guid DoorId { get; init; }
        public DoorDataType Door { get; init; }

        public string UserId { get; init; }
        public UserDataType User { get; init; }

        public DateTimeOffset CreatedDate { get; init; }
    }
}
