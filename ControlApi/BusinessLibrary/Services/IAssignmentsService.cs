using DataAccessLibrary;
using SharedLibrary.DataTypes.Assignment;
using SharedLibrary.DataTypes.Doors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services
{
    public interface IAssignmentsService
    {
        Task<PaginationDataType<AssignmentDataType>> GetAll(int Skip, int Take);

        Task<AssignmentDataType> Create(CreateAssignmentRequestDataType Data);

        Task<AssignmentDataType> GetById(Guid Id);

        Task<PaginationDataType<DoorDataType>> GetDoors(int Skip, int Take);

        Task Delete(Guid Id);
    }
}
