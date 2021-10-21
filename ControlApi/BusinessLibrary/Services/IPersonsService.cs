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
        IEnumerable<PersonDataType> GetAll();

        PersonDataType GetById(Guid Id);

        void Create(CreatePersonDataType Data);
    }
}
