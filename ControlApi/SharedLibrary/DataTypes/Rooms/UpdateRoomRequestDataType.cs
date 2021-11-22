using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Rooms
{
    public record UpdateRoomRequestDataType
    {
        public Guid? BuildingId { get; init; }
    }
}
