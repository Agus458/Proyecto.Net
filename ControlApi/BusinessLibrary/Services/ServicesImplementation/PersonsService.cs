using DataAccessLibrary.Entities;
using DataAccessLibrary.Stores;
using SharedLibrary.DataTypes.Persons;
using SharedLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services.ServicesImplementation
{
    public class PersonsService : IPersonsService
    {
        private readonly IPersonsStore Store;

        public PersonsService(IPersonsStore Store)
        {
            this.Store = Store;
        }

        public void Create(CreatePersonDataType Data)
        {
            if (Data == null) throw new ArgumentNullException();

            var NewPerson = new Person();
            NewPerson.AssignDataType<CreatePersonDataType>(Data);

            this.Store.Create(NewPerson);
        }

        public IEnumerable<PersonDataType> GetAll()
        {
            return this.Store.GetAll().Select(Person => Person.GetDataType());
        }

        public PersonDataType GetById(Guid Id)
        {
            return this.Store.GetById(Id).GetDataType();
        }
    }
}
