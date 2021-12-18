using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Tenants
{
    public record UpdateTenantRequestDataType
    {
        [MaxLength(12)]
        public string Rut { get; init; }

        [MaxLength(200)]
        public string SocialReason { get; init; }

        public Guid ProductId { get; set; }
    }
}
