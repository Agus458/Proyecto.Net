using DataAccessLibrary.Contexts;
using DataAccessLibrary.Entities;
using DataAccessLibrary.Extensions;
using SharedLibrary.DataTypes.Institutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Stores.StoresImplementations
{
    public class InstitutionsStore : Store<Institution>, IInstitutionsStore
    {
        private readonly ApiDbContext Context;

        public InstitutionsStore(ApiDbContext Context) : base(Context)
        {
            this.Context = Context;
        }

        public InstitutionDataType GetByRut(string Rut)
        {
            var Institution = this.Context.Set<Institution>().FirstOrDefault(ExistingInstitution => ExistingInstitution.Rut == Rut);

            if (Institution != null) return Institution.GetDataType();

            return null;
        }

        public InstitutionDataType GetBySocialReason(string SocialReason)
        {
            var Institution = this.Context.Set<Institution>().FirstOrDefault(ExistingInstitution => ExistingInstitution.SocialReason == SocialReason);

            if (Institution != null) return Institution.GetDataType();

            return null;
        }
    }
}
