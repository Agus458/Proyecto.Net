using DataAccessLibrary;
using SharedLibrary.DataTypes.Doors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services
{
    public interface IDoorsService
    {
        PaginationDataType<DoorDataType> GetAll(int Skip, int Take);

        DoorDataType GetById(Guid Id);

        DoorDataType Create(CreateDoorRequestDataType Data);

        void Delete(Guid Id);

        void Update(Guid Id, UpdateDoorRequestDataType Data);
    }
}
