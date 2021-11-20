using System;
using DataAccessLibrary;
using SharedLibrary.DataTypes.Precio;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary.Entities;

namespace BusinessLibrary.Services
{
    public interface IPrecioService
    {
        PaginationDataType<PrecioDataType> GetAll(int skup, int Take, Guid BuildingId);


        PrecioDataType GetBuId(Guid Id);

        PrecioDataType Create(CreatePrecioRequestDataType Data);

        void Delete(Guid Id);

        void Update(Guid Id, UpdatePreciosRequestDataType Data);
        

    }
}
