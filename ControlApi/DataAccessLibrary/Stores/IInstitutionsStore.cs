using DataAccessLibrary.Entities;
using SharedLibrary.DataTypes.Institutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Stores
{
    /// <summary>
    /// Store Interface to manage the institutions in the database.
    /// </summary>
    public interface IInstitutionsStore : IStore<Institution>
    {
        InstitutionDataType GetByRut(string Rut);

        InstitutionDataType GetBySocialReason(string Rut);
    }
}
