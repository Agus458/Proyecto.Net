using SharedLibrary.DataTypes;
using SharedLibrary.DataTypes.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services
{
    public interface IEntriesService
    {
        PaginationDataType<EntryDataType> GetAll(int Skip, int Take, Guid BuildingId);

        Task<PaginationDataType<EntryDataType>> Get(int Skip, int Take);

        Task<EntryDataType> GetById(Guid Id);

        Task<EntryDataType> Create(CreateEntryRequestDataType Data);

        Task Delete(Guid Id);
    }
}
