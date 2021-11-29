using DataAccessLibrary;
using SharedLibrary.DataTypes.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services
{
    public interface IRoomsService
    {
        PaginationDataType<RoomDataType> GetAll(int Skip, int Take, Guid BuildingId);

        RoomDataType GetById(Guid Id);

        RoomDataType Create(CreateRoomRequestDataType Data);

        void Delete(Guid Id);

        void Update(Guid Id, UpdateRoomRequestDataType Data);
    }
}
