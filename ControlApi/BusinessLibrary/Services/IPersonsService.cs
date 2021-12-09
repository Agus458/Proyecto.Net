using DataAccessLibrary;
using Microsoft.AspNetCore.Http;
using SharedLibrary.DataTypes.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services
{
    /// <summary>
    /// Service that provides persons manipulation.
    /// </summary>
    public interface IPersonsService
    {
        PaginationDataType<PersonDataType> GetAll(int Skip, int Take);

        PersonDataType GetById(Guid Id);

        Task<PersonDataType> Create(CreatePersonRequestDataType Data);

        void Delete(Guid Id);

        Task Update(Guid Id, UpdatePersonRequestDataType Data);

        Task<PersonDataType> Identify(IFormFile fileImage);
    }
}
