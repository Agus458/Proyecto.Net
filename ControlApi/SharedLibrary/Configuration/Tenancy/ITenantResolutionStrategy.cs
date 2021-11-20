using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Configuration.Tenancy
{
    public interface ITenantResolutionStrategy
    {
        string GetTenantIdentifier();
    }
}
