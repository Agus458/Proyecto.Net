using SharedLibrary.DataTypes.Precio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services
{
   public interface IPrecioService
    {
        IEnumerable<PrecioDatatype> GetAll();

        PrecioDatatype GetById(Guid Id);

        Task<dynamic> Create(CreatePrecioDataType Data);
    }
}
