using DataAccessLibrary.Contexts;
using DataAccessLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Stores.StoresImplementations
{
    public class DoorsStore : Store<Door>, IDoorsStore
    {
        public DoorsStore(ApiDbContext Context) : base(Context)
        {

        }
    }
}
