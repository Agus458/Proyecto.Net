using SharedLibrary.DataTypes.Factura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services
{
    public interface IFacturaService
    {
        IEnumerable<FacturaDataType> GetAll();

        FacturaDataType GetById(Guid Id);
       
        Task<dynamic> Create(CreateFacturaDataType Data);
    }
}
