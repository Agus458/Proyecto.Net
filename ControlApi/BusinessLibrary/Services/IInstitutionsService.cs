using SharedLibrary.DataTypes.Institutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services
{
    public interface IInstitutionsService
    {
        IEnumerable<InstitutionDataType> GetAll();
        
        InstitutionDataType GetById(Guid Id);

        Task<dynamic> Create(CreateInstitutionRequestDataType Data);
    }
}
