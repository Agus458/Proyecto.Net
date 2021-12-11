using DataAccessLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Stores
{
    public interface IAssignmentsStore : IStore<Assignment>
    {
        PaginationDataType<Assignment> GetAll(int Skip, int Take, string UserId, [Optional] string[] Relations);

        Assignment GetById(Guid Id, string UserId, [Optional] string[] Relations);

        Assignment GetLast(string UserId, [Optional] string[] Relations);
    }
}
