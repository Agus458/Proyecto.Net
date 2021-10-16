using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Institutions
{
    public record InstitutionDataType
    {
        public Guid Id { get; init; }
        
        public string Rut { get; init; }

        public string SocialReason { get; init; }
    }
}
