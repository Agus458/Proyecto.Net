using System;
using DataAccessLibrary;
using SharedLibrary.DataTypes.Factura;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary.Entities;

namespace BusinessLibrary.Services
{
    public interface IFacturaService
    {
        PaginationDataType<FacturaDataType> GetAll(int skup, int Take, Guid BuildingId);


        FacturaDataType GetBuId(Guid Id);

        FacturaDataType Create(CreateFacturaRequestDataType Data);

        void Delete(Guid Id);

        void Update(Guid Id, UpdateFacturaRequestDataType Data);


    }
}
