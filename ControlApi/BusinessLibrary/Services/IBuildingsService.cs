using DataAccessLibrary;
using SharedLibrary.DataTypes.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services
{
    public interface IBuildingsService
    {
        PaginationDataType<BuildingDataType> GetAll(int Skip, int Take);

        IEnumerable<BuildingDataType> Get();

        BuildingDataType GetById(Guid Id);

        BuildingDataType Create(CreateBuildingRequestDataType Data);

        void Delete(Guid Id);

        void Update(Guid Id, UpdateBuildingRequestDataType Data);
    }
}
