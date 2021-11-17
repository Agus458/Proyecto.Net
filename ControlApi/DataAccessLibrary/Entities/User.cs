using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Entities
{
    public class User : IdentityUser
    {
        public Guid? TenantId { get; set; }

        public Tenant Tenant { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }
    }
}
