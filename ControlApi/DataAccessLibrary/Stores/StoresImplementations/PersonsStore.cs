using DataAccessLibrary.Contexts;
using DataAccessLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Stores.StoresImplementations
{
    public class PersonsStore : Store<Person>, IPersonsStore
    {
        private readonly MultiTenantDbContext Context;

        public PersonsStore(MultiTenantDbContext Context) : base(Context)
        {
            this.Context = Context;
        }
    }
}
