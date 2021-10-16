using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Tenants
{
    public record TenantDataType
    {
        public Guid Id { get; init; }
        
        public string Rut { get; init; }

        public string SocialReason { get; init; }
    }
}
