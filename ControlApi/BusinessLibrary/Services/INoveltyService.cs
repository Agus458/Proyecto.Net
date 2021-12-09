using DataAccessLibrary;
using SharedLibrary.DataTypes.Novelties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services
{
   public interface INoveltyService
    {
        PaginationDataType<NoveltyDataType> GetAll(int Skip, int Take, Guid BuildingId);

        NoveltyDataType GetById(Guid Id, Guid BuildingId);

        NoveltyDataType Create(CreateNoveltyRequestDataType Data);

        void Delete(Guid Id, Guid BuildingId);

        void Update(Guid Id, UpdateNoveltyRequestDataType Data);
    }
}
