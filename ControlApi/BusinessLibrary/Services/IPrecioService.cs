using System;
using SharedLibrary.DataTypes;
using SharedLibrary.DataTypes.Precio;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services
{
    public interface IPrecioService
    {
        PaginationDataType<PrecioDataType> GetAll(int skup, int Take,Guid ProductId);

        PrecioDataType Create(CreatePrecioRequestDataType Data);

        void Delete(Guid Id);

        void Update(Guid Id, UpdatePreciosRequestDataType Data);
        PrecioDataType GetBuId(Guid Id);
    }
}
