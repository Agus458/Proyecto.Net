using System;
using DataAccessLibrary;
using SharedLibrary.DataTypes.Pago;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary.Entities;

namespace BusinessLibrary.Services
{
    public interface IPagoService
    {
        PaginationDataType<PagoDataType> GetAll(int skup, int Take);

        PagoDataType GutById(Guid Id);

        PagoDataType Create(CreatePagoRequestDataType Data);


        void Delete(Guid Id);

        void Update(Guid Id, UpdatePagoRequestDateType Data);

    }
}
