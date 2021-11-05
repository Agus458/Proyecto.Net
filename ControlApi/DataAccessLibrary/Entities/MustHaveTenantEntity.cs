using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Entities
{
    /// <summary>
    /// Adds necesary attributes to entities that must have a Tenant.
    /// </summary>
    public class MustHaveTenantEntity : BaseEntity
    {
        /// <summary>
        /// The Id of the tenant that the entity belongs to.
        /// </summary>
        public virtual Guid TenantId { get; set; }
        
        /// <summary>
        /// The Tenant entity to which belongs.
        /// </summary>
        public virtual Tenant Tenant { get; set; }
    }
}
