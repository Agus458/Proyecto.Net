using SharedLibrary.DataTypes.Factura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services.ServicesImplementation
{
    public class FacturaService : IFacturaService
    {
        public Task<dynamic> Create(CreateFacturaDataType Data)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FacturaDataType> GetAll()
        {
            throw new NotImplementedException();
        }

        public FacturaDataType GetById(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
