using DataAccessLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Stores
{
    /// <summary>
    /// Store interface that implements basic CRUD operations for persons entities.
    /// </summary>
    public interface IPersonsStore : IStore<Person>
    {
    }
}
