using DataAccessLibrary.Contexts;
using DataAccessLibrary.Entities;
using SharedLibrary.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.DataTypes.Precio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services.ServicesImplementation
{

    public class PrecioService : IPrecioService
    {
        public Task<dynamic> Create(CreatePrecioDataType Data)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PrecioDatatype> GetAll()
        {
            throw new NotImplementedException();
        }

        public PrecioDatatype GetById(Guid Id)
        {
            throw new NotImplementedException();
        }
    }

}
