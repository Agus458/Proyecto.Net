using DataAccessLibrary;
using SharedLibrary.DataTypes.Assignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services
{
    public interface IAssignmentsService
    {
        PaginationDataType<AssignmentDataType> GetAll(int Skip, int Take);

        Task<AssignmentDataType> Create(Guid DoorId);

        AssignmentDataType GetById(Guid Id);
    }
}
