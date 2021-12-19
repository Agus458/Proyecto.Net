using System;
using SharedLibrary.DataTypes;
using SharedLibrary.DataTypes.Factura;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BusinessLibrary.Services
{
    public interface IFacturaService
    {
        PaginationDataType<FacturaDataType> GetAll(int skip, int Take);

        FacturaDataType GetBuId(Guid Id);

        FacturaDataType Create(CreateFacturaRequestDataType Data);

        void Delete(Guid Id);

        void Update(Guid Id, UpdateFacturaRequestDataType Data);

        void GenerateBills();

        void Pagar(Guid Id);
    }
}
